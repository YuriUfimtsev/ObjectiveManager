using AuthService.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationsService.Application.Services.Interfaces;
using NotificationsService.Domain.Entities;
using NotificationsService.Domain.Interfaces;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using ObjectivesService.Client;

namespace NotificationsService.Application.Services;

public class EmailSenderBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEmailSenderService _emailSenderService;

    public EmailSenderBackgroundService(IEmailSenderService emailSenderService,
        IServiceScopeFactory scopeFactory, IDateTimeProvider dateTimeProvider)
    {
        _emailSenderService = emailSenderService;
        _scopeFactory = scopeFactory;
        _dateTimeProvider = dateTimeProvider;
    }

    // Сервис начинает проверку базы уведомлений в 11:55 ежедневно.
    // Отправляет письма при необходимости и засыпает до следующего дня.
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var notificationsRepository = scope.ServiceProvider.GetRequiredService<INotificationsRepository>();
            var objectivesServiceClient = scope.ServiceProvider.GetRequiredService<IObjectivesServiceClient>();
            var authServiceClient = scope.ServiceProvider.GetRequiredService<IAuthServiceClient>();
            var emailBuilderService = scope.ServiceProvider.GetRequiredService<IEmailBuilderService>();

            var notifications = await notificationsRepository.GetAll();
            var nowForNotify = _dateTimeProvider.Now();
            var emailTasks = new List<Task>();

            foreach (var notification in notifications)
            {
                if (notification.NextNotificationTime.Year == nowForNotify.Year
                    && notification.NextNotificationTime.Month == nowForNotify.Month
                    && notification.NextNotificationTime.Day == nowForNotify.Day)
                {
                    emailTasks.Add(
                        ProcessEmailsSending(
                            notification,
                            notificationsRepository,
                            emailBuilderService,
                            objectivesServiceClient,
                            authServiceClient,
                            cancellationToken));
                }
            }

            Task.WaitAll(emailTasks.ToArray());

            var delay = CalculateBackgroundServiceDelay();
            await Task.Delay(delay, cancellationToken);
        }
    }

    private async Task ProcessEmailsSending(
        NotificationEntity notification,
        INotificationsRepository notificationsRepository,
        IEmailBuilderService emailBuilderService,
        IObjectivesServiceClient objectivesServiceClient,
        IAuthServiceClient authServiceClient,
        CancellationToken cancellationToken)
    {
        var objectives = await objectivesServiceClient.GetAllUserObjectives(notification.UserId);
        if (!objectives.Any())
        {
            return;
        }

        var user = await authServiceClient.GetUserData(notification.UserId);

        // Создаем структуру письма для пользователя (без истории статусов) и отправляем
        var userEmailModel = await emailBuilderService.BuildForWorker(objectives);
        await _emailSenderService.SendEmailWithRetriesAsync(user.Value.Email, userEmailModel, cancellationToken);

        // Создаем структуру письма для менторов (с историей статусов) и отправляем
        var mentorEmailModel =
            await emailBuilderService.BuildForMentor(user.Value, objectives);
        await _emailSenderService.SendEmailWithRetriesAsync(user.Value.MentorEmail, mentorEmailModel,
            cancellationToken);

        // Выставляем время отправки следующей пары нотификаций
        await UpdateNextNotificationTime(notification, notificationsRepository);
    }

    private TimeSpan CalculateBackgroundServiceDelay()
    {
        var now = _dateTimeProvider.Now();
        var nextBackgroundServiceRun = new DateTime(
            now.Year,
            now.Month,
            now.AddDays(1).Day,
            11, 55, 0);
        return nextBackgroundServiceRun - _dateTimeProvider.Now();
    }

    private async Task UpdateNextNotificationTime(NotificationEntity currentNotification,
        INotificationsRepository notificationsRepository)
    {
        if (currentNotification.FrequencyValue == null)
        {
            throw new ApplicationException(
                $"Ошибка при получении информации о частоте отправки уведомлений для пользователя {currentNotification.UserId}");
        }

        var valueIntervalInHours = currentNotification.FrequencyValue.IntervalInHours;
        var nextNotificationTime = currentNotification.NextNotificationTime.AddHours(valueIntervalInHours);
        var affectedRows = await notificationsRepository.UpdateNextNotificationTime(currentNotification.Id, nextNotificationTime);

        if (affectedRows == 0)
        {
            throw new ApplicationException(
                $"Ошибка попытке установить время следующей нотификации для пользователя {currentNotification.UserId}");
        }
    }
}
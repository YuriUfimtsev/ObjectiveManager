using AutoMapper;
using NotificationsService.Application.Services.Interfaces;
using NotificationsService.DataAccess.Models;
using NotificationsService.Domain;
using NotificationsService.Domain.Entities;
using NotificationsService.Domain.Interfaces;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using ObjectiveManager.Models.NotificationsService.DTO;
using ObjectiveManager.Models.Result;

namespace NotificationsService.Application.Services;

public class NotificationsService : INotificationsService
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public NotificationsService(INotificationsRepository notificationsRepository, IMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _notificationsRepository = notificationsRepository;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Create(string userId)
    {
        var defaultFrequencyId = DefaultFrequency.Id;

        var now = _dateTimeProvider.Now();
        var nextNotificationTime = NotificationTimeHelper.GetNextMondayNoon(now);
        var notification = new NotificationEntity
        {
            UserId = userId,
            NextNotificationTime = nextNotificationTime,
            FrequencyValueId = defaultFrequencyId
        };
        
        var notificationId = await _notificationsRepository.Create(notification);

        return notificationId;
    }

    public async Task<Result<NotificationDTO>> GetForUser(string userId)
    {
        var notification = await _notificationsRepository.GetForUser(userId);
        if (notification == null)
        {
            return Result<NotificationDTO>.Failed(
                $"Информация об уведомлениях для пользователя с идентификатором {userId} не найдена");
        }

        var notificationDto = _mapper.Map<NotificationDTO>(notification);
        return Result<NotificationDTO>.Success(notificationDto);
    }

    public async Task<Result> UpdateFrequency(Guid id, long frequencyValueId)
    {
        var rowsAffectedCount = await _notificationsRepository.UpdateFrequency(id, frequencyValueId);
        return rowsAffectedCount == 0
            ? Result.Failed($"Цель с идентификатором {id} не найдена")
            : Result.Success();
    }
}
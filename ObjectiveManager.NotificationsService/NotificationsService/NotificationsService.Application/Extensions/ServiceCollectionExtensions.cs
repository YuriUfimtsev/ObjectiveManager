using System.Net.Mail;
using AuthService.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationsService.Application.Configuration;
using NotificationsService.Application.Mapping;
using NotificationsService.Application.Services;
using NotificationsService.Application.Services.Interfaces;
using NotificationsService.DataAccess.Extensions;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using ObjectiveManager.Utils.Configuration;
using ObjectivesService.Client;

namespace NotificationsService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.ConfigureObjectiveManagerServices("Notifications API");
        services.AddDataAccessInfrastructure(configuration);

        services.AddHttpClient();
        services.AddObjectivesServiceClient();
        services.AddAuthServiceClient();

        services.Configure<EmailSenderConfiguration>(configuration.GetSection("EmailSenderConfiguration"));
        AddApplicationLayerServices(services);
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }

    private static void AddApplicationLayerServices(IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IFrequencyValuesService, FrequencyValuesService>();
        services.AddScoped<INotificationsService, Services.NotificationsService>();
        services.AddScoped<IEmailBuilderService, EmailBuilderService>();
        
        services.AddSingleton<IEmailSenderService, EmailSenderService>();
        services.AddSingleton<SmtpClient>();
        services.AddHostedService<EmailSenderBackgroundService>();
    }
}
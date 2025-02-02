using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationsService.Application.Mapping;
using NotificationsService.Application.Services;
using NotificationsService.Application.Services.Interfaces;
using NotificationsService.DataAccess.Extensions;
using ObjectiveManager.Utils.Configuration;

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
        //services.AddAuthServiceClient();

        AddApplicationLayerServices(services);
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }

    private static void AddApplicationLayerServices(IServiceCollection services)
    {
        services.AddScoped<IFrequencyValuesService, FrequencyValuesService>();
        services.AddScoped<INotificationsService, Services.NotificationsService>();
    }   
}
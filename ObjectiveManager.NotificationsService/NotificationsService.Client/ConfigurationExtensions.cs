using Microsoft.Extensions.DependencyInjection;

namespace NotificationsService.Client;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddObjectivesServiceClient(this IServiceCollection services)
    {
        services.AddScoped<INotificationsServiceClient, NotificationsServiceClient>();
        return services;
    }
}
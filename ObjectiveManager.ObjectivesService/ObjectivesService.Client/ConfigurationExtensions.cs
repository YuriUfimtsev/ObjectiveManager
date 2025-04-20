using Microsoft.Extensions.DependencyInjection;

namespace ObjectivesService.Client;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddObjectivesServiceClient(this IServiceCollection services)
    {
        services.AddScoped<IObjectivesServiceClient, ObjectivesServiceClient>();
        return services;
    }
}
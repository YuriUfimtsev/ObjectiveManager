using System.IdentityModel.Tokens.Jwt;
using AuthService.Client;
using NotificationsService.Client;
using ObjectiveManager.Utils.Configuration;
using ObjectivesService.Client;

namespace APIGateway.Api.Extensions;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddSingleton<JwtSecurityTokenHandler>();
        services.ConfigureObjectiveManagerServices("API Gateway");
        services.AddHttpClient();
        services.AddObjectivesServiceClient();
        services.AddAuthServiceClient();
        services.AddNotificationsServiceClient();

        return services;
    }
}
using AuthService.Client;
using Microsoft.IdentityModel.Tokens;
using ObjectiveManager.Utils.Auth;
using ObjectiveManager.Utils.Configuration;
using ObjectivesService.Client;

namespace APIGateway.Api.Extensions;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.ConfigureObjectiveManagerServices("API Gateway");
        services.AddHttpClient();
        services.AddObjectivesServiceClient();
        services.AddAuthServiceClient();

        return services;
    }
}
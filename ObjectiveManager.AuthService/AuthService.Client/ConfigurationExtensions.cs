using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Client;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddAuthServiceClient(this IServiceCollection services)
    {
        services.AddScoped<IAuthServiceClient, AuthServiceClient>();
        return services;
    }
}
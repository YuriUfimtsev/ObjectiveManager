using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObjectiveManager.DataAccess;
using ObjectiveManager.DataAccess.Extensions;

namespace ObjectiveManager.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.AddDataAccessRepositories();
        services.AddDataAccessInfrastructure(configuration);
        services.AddSingleton<IObjectiveService, ObjectiveService>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
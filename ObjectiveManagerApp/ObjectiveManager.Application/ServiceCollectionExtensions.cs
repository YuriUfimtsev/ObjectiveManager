using Microsoft.Extensions.DependencyInjection;
using ObjectiveManager.DataAccess;

namespace ObjectiveManager.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddSingleton<IObjectiveService, ObjectiveService>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
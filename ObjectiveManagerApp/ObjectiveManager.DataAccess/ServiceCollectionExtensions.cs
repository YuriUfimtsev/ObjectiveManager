using Microsoft.Extensions.DependencyInjection;
using ObjectiveManager.Domain;

namespace ObjectiveManager.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
        => services.AddSingleton<IObjectiveRepository, ObjectiveCsvRepository>();
}
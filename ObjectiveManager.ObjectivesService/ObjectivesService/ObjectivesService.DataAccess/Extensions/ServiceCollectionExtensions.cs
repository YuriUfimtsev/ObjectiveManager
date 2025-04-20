using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using ObjectivesService.DataAccess.Models;
using ObjectivesService.DataAccess.Repositories;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IObjectiveRepository, ObjectivePostgresRepository>();
        services.AddScoped<IObjectiveStatusRepository, ObjectiveStatusRepository>();
        services.AddScoped<IStatusObjectRepository, StatusObjectRepository>();
    }
    
    public static IServiceCollection AddDataAccessInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        var objectivesConnectionString = configuration.GetConnectionString("ObjectivesServiceDB");
        services.AddDbContext<ObjectivesContext>(options => options.UseNpgsql(objectivesConnectionString));
        AddRepositories(services);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
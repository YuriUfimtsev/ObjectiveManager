using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObjectiveManager.DataAccess.Models;
using ObjectiveManager.DataAccess.Repositories;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessRepositories(this IServiceCollection services)
    {
        services.AddScoped<IObjectiveRepository, ObjectivePostgresRepository>();
        services.AddScoped<IObjectiveStatusRepository, ObjectiveStatusRepository>();
        
        return services;
    }

    public static IServiceCollection AddDataAccessInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<ObjectivesContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ObjectiveManager.DataAccess.Models;
using ObjectiveManager.DataAccess.Repositories;
using ObjectiveManager.DataAccess.Settings;
using ObjectiveManager.Domain;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessRepositories(this IServiceCollection services)
    {
        services.AddScoped<IObjectiveRepository, ObjectivePostgresRepository>();
        
        return services;
    }

    public static IServiceCollection AddDataAccessInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        var optionsSection = configuration.GetSection(nameof(DataAccessOptions));
        services.Configure<DataAccessOptions>(optionsSection);

        var connectionString = configuration.GetConnectionString("PostgresConnectionString");
        services.AddDbContext<ObjectivesContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }
}
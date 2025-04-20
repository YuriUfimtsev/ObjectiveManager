using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationsService.DataAccess.Models;
using NotificationsService.DataAccess.Repositories;
using NotificationsService.Domain.Interfaces;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using IDateTimeProvider = ObjectiveManager.Models.EntityFramework.Infrastructure.IDateTimeProvider;

namespace NotificationsService.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IFrequencyValuesRepository, FrequencyValuesRepository>();
        services.AddScoped<INotificationsRepository, NotificationsRepository>();
    }
    
    public static IServiceCollection AddDataAccessInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        var objectivesConnectionString = configuration.GetConnectionString("NotificationsServiceDB");
        services.AddDbContext<NotificationsContext>(options => options.UseNpgsql(objectivesConnectionString));
        AddRepositories(services);
        
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
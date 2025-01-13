using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ObjectiveManager.Application.Services;
using ObjectiveManager.DataAccess.Extensions;

namespace ObjectiveManager.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.AddDataAccessRepositories();
        services.AddDataAccessInfrastructure(configuration);
        
        services.AddScoped<IStatusesService, StatusesService>();
        services.AddScoped<IObjectiveService, ObjectiveService>();
        
        services.AddAutoMapper(typeof(ObjectiveManager.Application.MappingProfile));
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        return services;
    }
}
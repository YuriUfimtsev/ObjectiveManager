using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<IObjectiveService, ObjectiveService>();
        services.AddAutoMapper(typeof(MappingProfile));
        
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
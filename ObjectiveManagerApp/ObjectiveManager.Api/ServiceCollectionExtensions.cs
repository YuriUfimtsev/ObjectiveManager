using ObjectiveManager.Application;

namespace ObjectiveManager.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Api.MappingProfile));
        services.AddSwaggerGen();
        services.AddControllers();
        
        services.AddApplicationServices();

        return services;
    }
}
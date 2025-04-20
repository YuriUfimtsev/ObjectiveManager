using AuthService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("AuthServiceDB");
        services.AddDbContext<IdentityContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}
using AuthService.Application.Services;
using AuthService.Application.Services.Interfaces;
using AuthService.DataAccess.Extensions;
using AuthService.DataAccess.Models;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ObjectiveManager.Utils.Auth;
using ObjectiveManager.Utils.Configuration;

namespace AuthService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.ConfigureObjectiveManagerServices("AuthService API");
        services.AddDataAccessInfrastructure(configuration);
        AddAuthInfrastructure(services);
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }

    private static void AddAuthInfrastructure(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddIdentityCore<User>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<User>>();
        
        services.AddScoped<IAuthTokenService, AuthTokenService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IUserManager, ProxyUserManager>();
    }
}
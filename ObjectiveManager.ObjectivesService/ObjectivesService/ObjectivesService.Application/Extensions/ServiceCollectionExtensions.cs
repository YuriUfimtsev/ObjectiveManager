using AuthService.Client;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ObjectiveManager.Utils.Auth;
using ObjectiveManager.Utils.Configuration;
using ObjectivesService.Application.Mapping;
using ObjectivesService.Application.Services;
using ObjectivesService.Application.Services.Interfaces;
using ObjectivesService.DataAccess.Extensions;

namespace ObjectivesService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.ConfigureObjectiveManagerServices("Objectives API");
        services.AddDataAccessInfrastructure(configuration);

        services.AddHttpClient();
        services.AddAuthServiceClient();

        AddApplicationLayerServices(services);
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }

    private static void AddApplicationLayerServices(IServiceCollection services)
    {
        services.AddScoped<IStatusValueService, StatusValueService>();
        services.AddScoped<IObjectiveService, ObjectiveService>();
        services.AddScoped<IStatusObjectService, StatusObjectService>();
    }   
}
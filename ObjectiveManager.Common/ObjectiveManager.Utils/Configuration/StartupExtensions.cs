using dotenv.net;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ObjectiveManager.Utils.Auth;

namespace ObjectiveManager.Utils.Configuration;

public static class StartupExtensions
{
    public static IServiceCollection ConfigureObjectiveManagerServices(this IServiceCollection services,
        string serviceName)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddCors()
            .AddMvc()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        services.AddControllers();
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = serviceName, Version = "v1" });

            if (serviceName == "API Gateway")
            {
                swaggerGenOptions.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                swaggerGenOptions.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
            }
        });

        // TODO: вынести securityKey в один .env файл для всех сервисов
        DotEnv.Load();
        var result = EnvReader.HasValue("AppSecurityKey");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "AuthService",
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthenticationKey.GetSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        services.AddAuthorization();
        
        services.AddHttpContextAccessor();
        return services;
    }

    public static IApplicationBuilder ConfigureObjectiveManagerApp(
        this IApplicationBuilder app, IHostEnvironment env,
        string serviceName, DbContext? context = null)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", serviceName); });
        }
        else
        {
            app.UseHsts();
        }

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials());

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        if (context != null)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                return app;
            }

            var logger = app.ApplicationServices?
                .GetService<ILoggerFactory>()?
                .CreateLogger(typeof(StartupExtensions));

            var tries = 0;
            const int maxTries = 10;

            while (!context.Database.CanConnect() && ++tries <= maxTries)
            {
                var warningMessage = $"Can't connect to database. Try {tries}.";
                if (logger is null)
                {
                    Console.WriteLine(warningMessage);
                }
                else
                {
                    logger.LogWarning(warningMessage);
                }

                Thread.Sleep(5000);
            }

            if (tries > maxTries) throw new Exception("Can't connect to database");
            context.Database.Migrate();
        }

        return app;
    }
}
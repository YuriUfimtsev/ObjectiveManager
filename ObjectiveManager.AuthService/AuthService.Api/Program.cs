using AuthService.Application.Extensions;
using AuthService.DataAccess.Models;
using ObjectiveManager.Utils.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
    app.ConfigureObjectiveManagerApp(app.Environment, "AuthService API", identityContext);
}

app.Run();
using ObjectiveManager.Utils.Configuration;
using ObjectivesService.Application.Extensions;
using ObjectivesService.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var identityContext = scope.ServiceProvider.GetRequiredService<ObjectivesContext>();
    app.ConfigureObjectiveManagerApp(app.Environment, "ObjectivesService API", identityContext);
}

app.Run();
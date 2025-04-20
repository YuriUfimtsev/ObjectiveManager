using NotificationsService.Application.Extensions;
using NotificationsService.DataAccess.Models;
using ObjectiveManager.Utils.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var notificationsContext = scope.ServiceProvider.GetRequiredService<NotificationsContext>();
    app.ConfigureObjectiveManagerApp(app.Environment, "NotificationsService API", notificationsContext);
}

app.Run();
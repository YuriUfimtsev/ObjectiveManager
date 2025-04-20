using APIGateway.Api.Extensions;
using ObjectiveManager.Utils.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddApplicationServices();

var app = builder.Build();
app.ConfigureObjectiveManagerApp(app.Environment, "API Gateway");
app.Run();
using ObjectiveManager.Application;
using ObjectiveManager.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ObjectiveManager.Api.MappingProfile));
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.MapControllers();

app.MigrateUp();

app.Run();
using ObjectiveManager.Application;
using ObjectiveManager.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ObjectiveManager.Api.MappingProfile));
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(cpb => cpb.AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MigrateUp();

app.Run();
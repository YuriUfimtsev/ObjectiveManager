using ObjectiveManager.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(cpb => cpb.AllowAnyOrigin());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
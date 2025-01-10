using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ObjectiveManager.DataAccess.Models;
using Microsoft.Extensions.Hosting;

namespace ObjectiveManager.DataAccess.Extensions;

public static class HostExtensions
{
    public static IHost MigrateUp(this IHost app)
    {
        using var scope = app.Services.CreateScope();

        var tries = 0;
        const int maxTries = 10;

        var context = scope.ServiceProvider.GetRequiredService<ObjectivesContext>();

        while (!context.Database.CanConnect() && ++tries <= maxTries)
        {
            Console.WriteLine($"Can't connect to database. Try {tries}.");
            Thread.Sleep(5000);
        }

        var c = context.Database.CanConnect();
        if (tries > maxTries) throw new Exception("Can't connect to database");
        
        context.Database.Migrate();

        return app;
    }
}
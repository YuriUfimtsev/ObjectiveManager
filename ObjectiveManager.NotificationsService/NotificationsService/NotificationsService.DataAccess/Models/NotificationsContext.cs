using Microsoft.EntityFrameworkCore;
using NotificationsService.Domain.Entities;
using ObjectiveManager.Models.EntityFramework.Infrastructure;

namespace NotificationsService.DataAccess.Models;

public class NotificationsContext : DbContext
{
    public DbSet<NotificationEntity> Notifications { get; set; }
    public DbSet<FrequencyValueEntity> FrequencyValues { get; set; }

    public NotificationsContext(DbContextOptions<NotificationsContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FrequencyValueEntity>().HasData(
            new FrequencyValueEntity { Id = 1, Name = "Раз в день", IntervalInHours = 24 },
            new FrequencyValueEntity { Id = 2, Name = "Раз в неделю", IntervalInHours = 168 },
            new FrequencyValueEntity { Id = 3, Name = "Раз в две недели", IntervalInHours = 336 },
            new FrequencyValueEntity { Id = 1, Name = "Раз в четыре недели", IntervalInHours = 672 }
        );
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>();
    }
}
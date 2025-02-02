using Microsoft.EntityFrameworkCore;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using ObjectivesService.Domain.Entities;

namespace ObjectivesService.DataAccess.Models;

public class ObjectivesContext : DbContext
{
    public DbSet<ObjectiveEntity> Objectives { get; set; }
    public DbSet<StatusValueEntity> StatusValues { get; set; }
    public DbSet<StatusObjectEntity> StatusObjects { get; set; }
    
    public ObjectivesContext(DbContextOptions<ObjectivesContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<StatusValueEntity>().HasData(
            new StatusValueEntity { Id = 1, Name = "Создана" },
            new StatusValueEntity { Id = 2, Name = "Приостановлена" },
            new StatusValueEntity { Id = 3, Name = "Не достигнута" },
            new StatusValueEntity { Id = 4, Name = "Достигнута" }
        );
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>();
    }
}
using Microsoft.EntityFrameworkCore;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.DataAccess.Models;

public class ObjectivesContext : DbContext
{
    public DbSet<ObjectiveEntity> Objectives { get; set; }
    public DbSet<ObjectiveStatusEntity> ObjectiveStatuses { get; set; }

    public ObjectivesContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ObjectiveStatusEntity>().HasData(
            new ObjectiveStatusEntity { Id = 1, Name = "Создана" },
            new ObjectiveStatusEntity { Id = 2, Name = "Приостановлена" },
            new ObjectiveStatusEntity { Id = 3, Name = "Не достигнута" },
            new ObjectiveStatusEntity { Id = 4, Name = "Достигнута" }
        );
    }
}
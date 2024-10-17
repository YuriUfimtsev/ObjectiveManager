using Microsoft.EntityFrameworkCore;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.DataAccess.Models;

public class ObjectivesContext : DbContext
{
    public DbSet<ObjectiveEntity> Objectives { get; set; }

    public ObjectivesContext(DbContextOptions options)
        : base(options)
    {
    }
}
using Microsoft.EntityFrameworkCore;
using ObjectiveManager.DataAccess.Models;
using ObjectiveManager.Domain.Entities;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.DataAccess.Repositories;

public class ObjectivePostgresRepository : CrudRepository<Guid, ObjectiveEntity>,
    IObjectiveRepository
{
    public ObjectivePostgresRepository(ObjectivesContext context)
        : base(context)
    {
    }

    public async Task<Guid> Create(ObjectiveEntity objective) 
        => await AddAsync(objective);

    public async Task<ObjectiveEntity?> Get(Guid id)
        => await Context.Set<ObjectiveEntity>()
            .Include(obj => obj.Status)
            .FirstOrDefaultAsync(obj => obj.Id == id);

    public new async Task<List<ObjectiveEntity>> GetAll()
        => await Context.Set<ObjectiveEntity>()
            .Include(obj => obj.Status)
            .ToListAsync();

    public async Task Update(ObjectiveEntity updatedObjective)
    {
        await base.UpdateAsync(updatedObjective.Id,
            objective => new ObjectiveEntity
            {
                Definition = updatedObjective.Definition,
                FinalDate = updatedObjective.FinalDate,
                StatusId = updatedObjective.StatusId,
                Comment = updatedObjective.Comment
            });
    }

    public async Task Delete(Guid objectiveId)
        => await base.DeleteAsync(objectiveId);
}
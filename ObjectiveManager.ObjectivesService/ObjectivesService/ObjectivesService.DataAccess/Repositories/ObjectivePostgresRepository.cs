using Microsoft.EntityFrameworkCore;
using ObjectiveManager.Models.EntityFramework;
using ObjectivesService.DataAccess.Models;
using ObjectivesService.Domain.DTO;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.DataAccess.Repositories;

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
            .Include(obj => obj.StatusObject.StatusValue)
            .FirstOrDefaultAsync(obj => obj.Id == id);

    public async Task<List<ObjectiveEntity>> GetAllForUser(string userId)
        => await Context.Set<ObjectiveEntity>()
            .Include(obj => obj.StatusObject.StatusValue)
            .Where(obj => obj.UserId == userId)
            .ToListAsync();

    public async Task<int> Update(UpdateObjectiveDTO updatedObjective)
    {
        var objectiveId = updatedObjective.ObjectiveId;
        return await base.UpdateAsync(objectiveId,
            baseObjective => new ObjectiveEntity
            {
                CreatedAt = baseObjective.CreatedAt,
                UserId = baseObjective.UserId,
                StatusObjectId = baseObjective.StatusObjectId,
                
                Definition = updatedObjective.Definition,
                FinalDate = updatedObjective.FinalDate,
                Comment = updatedObjective.Comment
            });
    }

    public async Task<int> UpdateStatusObject(UpdateStatusObjectDTO updateStatusObjectDto) 
        => await base.UpdateAsync(updateStatusObjectDto.ObjectiveId,
            baseObjective => new ObjectiveEntity
            {
                CreatedAt = baseObjective.CreatedAt,
                UserId = baseObjective.UserId,
                Definition = baseObjective.Definition,
                FinalDate = baseObjective.FinalDate,
                Comment = baseObjective.Comment,
                
                StatusObjectId = updateStatusObjectDto.StatusObjectId
            });

    public async Task<int> Delete(Guid objectiveId)
        => await base.DeleteAsync(objectiveId);
}
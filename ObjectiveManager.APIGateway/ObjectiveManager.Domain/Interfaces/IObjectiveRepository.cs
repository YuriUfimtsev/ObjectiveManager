using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Domain.Interfaces;

public interface IObjectiveRepository
{
    public Task<Guid> Create(ObjectiveEntity newObjective);
    
    public Task<ObjectiveEntity?> Get(Guid id);
    
    public Task<List<ObjectiveEntity>> GetAll();
    
    public Task Update(ObjectiveEntity updatedObjective);
    
    public Task Delete(Guid id);
}
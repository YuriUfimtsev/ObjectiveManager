using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Domain.Interfaces;

public interface IObjectiveRepository
{
    public Task<string> Create(CreateObjectiveDto newObjective);
    
    public Task<ObjectiveEntity?> Get(string id);
    
    public List<ObjectiveEntity> GetAll();
    
    public Task Update(ObjectiveEntity updatedObjective);
    
    public Task Delete(string id);
}
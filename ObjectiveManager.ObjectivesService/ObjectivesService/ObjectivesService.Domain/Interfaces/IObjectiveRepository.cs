using ObjectivesService.Domain.DTO;
using ObjectivesService.Domain.Entities;

namespace ObjectivesService.Domain.Interfaces;

public interface IObjectiveRepository
{
    public Task<Guid> Create(ObjectiveEntity newObjective);
    
    public Task<ObjectiveEntity?> Get(Guid id);
    
    public Task<List<ObjectiveEntity>> GetAllForUser(string userId);
    
    public Task<int> Update(UpdateObjectiveDTO updatedObjective);
    
    public Task<int> UpdateStatusObject(UpdateStatusObjectDTO updateStatusObjectDto);
    
    public Task<int> Delete(Guid id);
}
using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Domain;

public interface IObjectiveRepository
{
    public string Create(CreateObjectiveDto newObjective);
    
    public ObjectiveEntity? Get(string id);
    
    public List<ObjectiveEntity> GetAll();
    
    public void Update(ObjectiveEntity updatedObjective);
    
    public void Delete(string id);
}
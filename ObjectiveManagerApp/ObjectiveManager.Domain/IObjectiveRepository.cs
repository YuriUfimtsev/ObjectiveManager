using ObjectiveManager.Domain.Models;

namespace ObjectiveManager.Domain;

public interface IObjectiveRepository
{
    public string Create(ObjectiveCreation newObjective);
    
    public Objective? Get(string id);
    
    public Objective Update(Objective updatedObjective);
    
    public Objective Delete(string id);
}
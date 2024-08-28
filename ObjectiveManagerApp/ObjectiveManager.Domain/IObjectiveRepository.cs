using ObjectiveManager.Domain.Models;

namespace ObjectiveManager.Domain;

public interface IObjectiveRepository
{
    public string Create(ObjectiveCreation newObjective);
    
    public Objective Get(string id);
    
    public Objective Update(Objective objective);
    
    public Objective Delete(string id);
}
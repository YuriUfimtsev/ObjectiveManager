using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Dto;

namespace ObjectiveManager.Application;

public interface IObjectiveService
{
    public string Create(CreateObjectiveDto newObjective);
    
    public Objective? Get(string id);
    
    public void Update(Objective updatedObjective);
    
    public void Delete(string id);
}
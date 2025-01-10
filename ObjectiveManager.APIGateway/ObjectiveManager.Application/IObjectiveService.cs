using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Dto;

namespace ObjectiveManager.Application;

public interface IObjectiveService
{
    public Task<string> Create(CreateObjectiveDto newObjective);
    
    public Objective? Get(string id);
    
    public List<Objective> GetAll();
    
    public void Update(Objective updatedObjective);
    
    public void Delete(string id);
}
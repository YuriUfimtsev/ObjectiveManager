using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Dto;

namespace ObjectiveManager.Application.Services;

public interface IObjectiveService
{
    public Task<string> Create(CreateObjectiveDto newObjective);
    
    public Objective? Get(string id);
    
    public List<Objective> GetAll();
    
    public Task Update(Objective updatedObjective);
    
    public Task Delete(string id);
}
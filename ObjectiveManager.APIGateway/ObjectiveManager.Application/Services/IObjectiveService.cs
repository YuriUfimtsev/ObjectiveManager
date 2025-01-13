using ObjectiveManager.Application.Dto;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Dto;

namespace ObjectiveManager.Application.Services;

public interface IObjectiveService
{
    public Task<string> Create(CreateObjectiveDto newObjective);
    
    public Task<Objective?> Get(string id);
    
    public Task<List<Objective>> GetAll();
    
    public Task Update(UpdateObjectiveDto updatedObjective);
    
    public Task Delete(string id);
}
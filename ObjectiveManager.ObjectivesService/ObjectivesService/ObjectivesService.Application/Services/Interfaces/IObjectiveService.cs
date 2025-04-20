using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectiveManager.Models.Result;
using ObjectivesService.Application.Dto;
using ObjectivesService.Domain.DTO;

namespace ObjectivesService.Application.Services.Interfaces;

public interface IObjectiveService
{
    public Task<Guid> CreateWithStatus(CreateObjectiveDto newObjective);
    
    public Task<Result<ObjectiveDTO>> Get(Guid id);
    
    public Task<Result<string>> GetCreatorId(Guid objectiveId);
    
    public Task<List<ObjectiveDTO>> GetAllForUser(string userId);
    
    public Task<Result> Update(UpdateObjectiveDTO updatedObjective);

    public Task<Result> UpdateStatusObject(UpdateStatusObjectDTO updateStatusObjectDto);
    
    public Task<Result> Delete(Guid id);
}
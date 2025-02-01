using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectiveManager.Models.Result;

namespace ObjectivesService.Client;

public interface IObjectivesServiceClient
{
    public Task<ObjectiveDTO[]> GetAllObjectives();
    public Task<Result<ObjectiveDTO>> GetObjectiveInfo(string objectiveId);
    public Task<string> CreateObjective(ObjectivePostDto objective);
    public Task<Result> DeleteObjective(string objectiveId);
    public Task<Result> UpdateObjectiveInfo(string objectiveId, ObjectivePutDto objectivePutDto);
    public Task<Result> UpdateObjectiveStatus(string objectiveId, StatusObjectPutDTO updateObjectDto);
    public Task<StatusObjectDTO[]> GetStatusesHistory(string objectiveId);
    public Task<StatusValueDTO[]> GetAllStatuses();
}
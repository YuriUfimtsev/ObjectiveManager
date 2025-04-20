using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectiveManager.Models.Result;

namespace ObjectivesService.Client;

public interface IObjectivesServiceClient
{
    public Task<ObjectiveDTO[]> GetAllUserObjectives(string userId = "");
    public Task<Result<ObjectiveDTO>> GetObjectiveInfo(string objectiveId);
    public Task<string> CreateObjective(ObjectivePostDto objective);
    public Task<Result> DeleteObjective(string objectiveId);
    public Task<Result> UpdateObjectiveInfo(string objectiveId, ObjectivePutDto objectivePutDto);
    public Task<Result> UpdateObjectiveStatus(string objectiveId, StatusObjectPutDTO updateObjectDto);
    public Task<Result<StatusObjectDTO[]>> GetStatusesHistory(string objectiveId, string userId = "");
    public Task<StatusValueDTO[]> GetAllStatuses();
}
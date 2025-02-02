using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Application.Dto;

namespace ObjectivesService.Application.Services.Interfaces;

public interface IStatusObjectService
{
    public Task<Guid> CreateObject(CreateStatusObjectDTO createStatusObjectDto);
    public Task<List<StatusObjectDTO>> GetHistory(Guid objectiveId);
}
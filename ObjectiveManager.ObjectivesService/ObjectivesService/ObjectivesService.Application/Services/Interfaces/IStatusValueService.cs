using ObjectiveManager.Models.ObjectivesService.DTO;

namespace ObjectivesService.Application.Services.Interfaces;

public interface IStatusValueService
{
    public Task<List<StatusValueDTO>> GetAll();
}
using ObjectiveManager.Application.Models;

namespace ObjectiveManager.Application.Services;

public interface IStatusesService
{
    public Task<List<ObjectiveStatus>> GetAll();
}
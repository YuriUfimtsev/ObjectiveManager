using ObjectiveManager.Models.NotificationsService.DTO;

namespace NotificationsService.Application.Services.Interfaces;

public interface IFrequencyValuesService
{
    public Task<List<FrequencyValueDTO>> GetAll();
}
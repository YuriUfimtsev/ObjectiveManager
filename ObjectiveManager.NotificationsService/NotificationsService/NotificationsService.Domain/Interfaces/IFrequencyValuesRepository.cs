using NotificationsService.Domain.Entities;
using ObjectiveManager.Models.EntityFramework;

namespace NotificationsService.Domain.Interfaces;

public interface IFrequencyValuesRepository : IReadOnlyRepository<long, FrequencyValueEntity>
{
}
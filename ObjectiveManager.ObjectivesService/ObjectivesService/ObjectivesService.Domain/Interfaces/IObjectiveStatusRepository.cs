using ObjectiveManager.Models.EntityFramework;
using ObjectivesService.Domain.Entities;

namespace ObjectivesService.Domain.Interfaces;

public interface IObjectiveStatusRepository : IReadOnlyRepository<long, StatusValueEntity>
{
}
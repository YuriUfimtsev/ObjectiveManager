using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Domain.Interfaces;

public interface IObjectiveStatusRepository : IReadOnlyRepository<long, ObjectiveStatusEntity>
{
}
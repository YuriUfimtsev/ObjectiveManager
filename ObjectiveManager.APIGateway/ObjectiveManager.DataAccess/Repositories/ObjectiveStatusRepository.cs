using ObjectiveManager.DataAccess.Models;
using ObjectiveManager.Domain.Entities;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.DataAccess.Repositories;

public class ObjectiveStatusRepository : ReadOnlyRepository<long, ObjectiveStatusEntity>, IObjectiveStatusRepository
{
    public ObjectiveStatusRepository(ObjectivesContext context)
        : base(context)
    {
    }
}
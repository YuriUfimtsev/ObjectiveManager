using ObjectiveManager.Models.EntityFramework;
using ObjectivesService.DataAccess.Models;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.DataAccess.Repositories;

public class ObjectiveStatusRepository : ReadOnlyRepository<long, StatusValueEntity>, IObjectiveStatusRepository
{
    public ObjectiveStatusRepository(ObjectivesContext context)
        : base(context)
    {
    }
}
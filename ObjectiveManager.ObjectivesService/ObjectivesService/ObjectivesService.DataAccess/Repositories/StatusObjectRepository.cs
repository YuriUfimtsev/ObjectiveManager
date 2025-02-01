using Microsoft.EntityFrameworkCore;
using ObjectivesService.DataAccess.Models;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.DataAccess.Repositories;

public class StatusObjectRepository: CrudRepository<Guid, StatusObjectEntity>, IStatusObjectRepository
{
    public StatusObjectRepository(ObjectivesContext context) : base(context)
    {
    }

    public async Task<List<StatusObjectEntity>> GetAllWithStatusValues(Guid objectiveId) 
        => await Context.Set<StatusObjectEntity>()
            .Include(s => s.StatusValue)
            .Where(s => s.ObjectiveId == objectiveId)
            .ToListAsync();
}
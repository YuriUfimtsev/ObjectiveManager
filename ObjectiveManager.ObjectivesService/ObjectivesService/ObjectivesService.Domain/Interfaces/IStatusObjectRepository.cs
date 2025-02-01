using ObjectivesService.Domain.Entities;

namespace ObjectivesService.Domain.Interfaces;

public interface IStatusObjectRepository: ICrudRepository<Guid, StatusObjectEntity>
{
    public Task<List<StatusObjectEntity>> GetAllWithStatusValues(Guid objectiveId);
}
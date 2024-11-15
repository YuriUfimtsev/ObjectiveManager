using ObjectiveManager.DataAccess.Models;
using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;
using ObjectiveManager.Domain.Enums;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.DataAccess.Repositories;

public class ObjectivePostgresRepository : CrudRepository<string, ObjectiveEntity>,
    IObjectiveRepository
{
    public ObjectivePostgresRepository(ObjectivesContext context)
        : base(context)
    {
    }

    public async Task<string> Create(CreateObjectiveDto newObjective)
    {
        var id = Guid.NewGuid().ToString();
        var objective = new ObjectiveEntity(
            Id: id,
            Definition: newObjective.Definition,
            FinalDate: newObjective.FinalDate,
            Status: ObjectiveStatus.Opened,
            Comment: newObjective.Comment);

        return await AddAsync(objective);
    }

    public async Task<ObjectiveEntity?> Get(string id)
        => await GetAsync(id);

    public new List<ObjectiveEntity> GetAll()
        => base.GetAll().ToList();

    public async Task Update(ObjectiveEntity updatedObjective)
    {
        // Пока передаём Id тоже, чтобы чистый record ObjectiveEntity в класс не преобразовывать
        await base.UpdateAsync(updatedObjective.Id,
            objective => new ObjectiveEntity(updatedObjective.Id,
                updatedObjective.Definition, updatedObjective.Status,
                updatedObjective.FinalDate, updatedObjective.Comment)
        );
    }

    public async Task Delete(string objectiveId)
        => await base.DeleteAsync(objectiveId);
}
using ObjectiveManager.Domain;
using ObjectiveManager.Domain.Models;

namespace ObjectiveManager.Application;

public class ObjectiveService : IObjectiveService
{
    private readonly IObjectiveRepository _objectiveRepository;

    public ObjectiveService(IObjectiveRepository objectiveRepository)
    {
        _objectiveRepository = objectiveRepository;
    }

    public string Create(ObjectiveCreation newObjective)
        => _objectiveRepository.Create(newObjective);

    public Objective? Get(string id)
        => _objectiveRepository.Get(id);

    public Objective Update(Objective updatedObjective)
        => _objectiveRepository.Update(updatedObjective);

    public Objective Delete(string id) 
        => _objectiveRepository.Delete(id);
}
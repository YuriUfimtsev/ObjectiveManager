using AutoMapper;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain;
using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Application;

public class ObjectiveService : IObjectiveService
{
    private readonly IObjectiveRepository _objectiveRepository;
    private readonly IMapper _mapper;

    public ObjectiveService(
        IObjectiveRepository objectiveRepository,
        IMapper mapper)
    {
        _objectiveRepository = objectiveRepository;
        _mapper = mapper;
    }

    public string Create(CreateObjectiveDto newObjective)
        => _objectiveRepository.Create(newObjective);

    public Objective? Get(string id)
    {
        var objectiveEntity = _objectiveRepository.Get(id);
        var objective = _mapper.Map<Objective>(objectiveEntity);

        return objective;
    }

    public void Update(Objective updatedObjective)
    {
        var objectiveEntity = _mapper.Map<ObjectiveEntity>(updatedObjective);
        _objectiveRepository.Update(objectiveEntity);
    }

    public void Delete(string id) 
        => _objectiveRepository.Delete(id);
}
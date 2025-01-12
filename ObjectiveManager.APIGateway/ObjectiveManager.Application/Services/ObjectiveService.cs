using AutoMapper;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.Application.Services;

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

    public async Task<string> Create(CreateObjectiveDto newObjective)
        => await _objectiveRepository.Create(newObjective);

    public Objective? Get(string id)
    {
        var objectiveEntity = _objectiveRepository.Get(id);
        var objective = _mapper.Map<Objective>(objectiveEntity);

        return objective;
    }

    public List<Objective> GetAll()
    {
        var objectiveEntities = _objectiveRepository.GetAll();
        var objectives = _mapper.Map<List<Objective>>(objectiveEntities);

        return objectives;
    }

    public async Task Update(Objective updatedObjective)
    {
        var objectiveEntity = _mapper.Map<ObjectiveEntity>(updatedObjective);
        await _objectiveRepository.Update(objectiveEntity);
    }

    public async Task Delete(string id) 
        => await _objectiveRepository.Delete(id);
}
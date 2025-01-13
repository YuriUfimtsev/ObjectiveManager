using AutoMapper;
using ObjectiveManager.Application.Dto;
using ObjectiveManager.Application.Models;
using ObjectiveManager.DataAccess.Models;
using ObjectiveManager.Domain.Dto;
using ObjectiveManager.Domain.Entities;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.Application.Services;

public class ObjectiveService : IObjectiveService
{
    private readonly IObjectiveRepository _objectiveRepository;
    private readonly IObjectiveStatusRepository _statusRepository;
    private readonly IMapper _mapper;

    public ObjectiveService(
        IObjectiveRepository objectiveRepository,
        IObjectiveStatusRepository objectiveStatusRepository,
        IMapper mapper)
    {
        _objectiveRepository = objectiveRepository;
        _statusRepository = objectiveStatusRepository;
        _mapper = mapper;
    }

    public async Task<string> Create(CreateObjectiveDto newObjective)
    {
        var status = await _statusRepository.FindAsync(st => st.Name == Statuses.Created);
        if (status == null)
        {
            throw new ApplicationException("В базе данных не найден подходящий статус для создания задачи");
        }
        
        var objective = new ObjectiveEntity
        {
            Definition = newObjective.Definition,
            FinalDate = newObjective.FinalDate,
            StatusId = status.Id,
            Comment = newObjective.Comment ?? string.Empty,
        };

        var objectiveId = await _objectiveRepository.Create(objective);
        return objectiveId.ToString();
    }

    public async Task<Objective?> Get(string id)
    {
        var objectiveEntity = await _objectiveRepository.Get(Guid.Parse(id));
        var objective = _mapper.Map<Objective>(objectiveEntity);

        return objective;
    }

    public async Task<List<Objective>> GetAll()
    {
        var objectiveEntities = await _objectiveRepository.GetAll();
        var objectives = _mapper.Map<List<Objective>>(objectiveEntities);

        return objectives;
    }

    public async Task Update(UpdateObjectiveDto updatedObjective)
    {
        var objective = new ObjectiveEntity
        {
            Id = Guid.Parse(updatedObjective.Id),
            Definition = updatedObjective.Definition,
            FinalDate = updatedObjective.FinalDate,
            StatusId = updatedObjective.StatusId,
            Comment = updatedObjective.Comment ?? string.Empty,
        };

        await _objectiveRepository.Update(objective);
    }

    public async Task Delete(string id) 
        => await _objectiveRepository.Delete(Guid.Parse(id));
}
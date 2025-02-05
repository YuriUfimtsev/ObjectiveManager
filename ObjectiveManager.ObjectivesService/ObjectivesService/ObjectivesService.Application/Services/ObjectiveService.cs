using AutoMapper;
using ObjectiveManager.Models.EntityFramework.Infrastructure;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectiveManager.Models.Result;
using ObjectivesService.Application.Dto;
using ObjectivesService.Application.Services.Interfaces;
using ObjectivesService.DataAccess.Models;
using ObjectivesService.Domain.DTO;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.Application.Services;

public class ObjectiveService : IObjectiveService
{
    private readonly IObjectiveRepository _objectiveRepository;
    private readonly IStatusObjectRepository _statusObjectRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public ObjectiveService(
        IObjectiveRepository objectiveRepository,
        IMapper mapper,
        IStatusObjectRepository statusObjectRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _objectiveRepository = objectiveRepository;
        _mapper = mapper;
        _statusObjectRepository = statusObjectRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> CreateWithStatus(CreateObjectiveDto newObjective)
    {
        // Создаем цель
        var objective = _mapper.Map<ObjectiveEntity>(newObjective);
        var objectiveId = await _objectiveRepository.Create(objective);

        // Создаём объект изменения статуса
        var statusObject = new StatusObjectEntity
        {
            CreatedAt = _dateTimeProvider.Now(),
            ObjectiveId = objectiveId,
            StatusValueId = StatusValues.CreatedId,
            Comment = string.Empty
        };
        var statusObjectId = await _statusObjectRepository.AddAsync(statusObject);

        // Обновляем объект статуса в цели
        var updateStatusObjectId = new UpdateStatusObjectDTO(
            ObjectiveId: objectiveId,
            StatusObjectId: statusObjectId);
        await _objectiveRepository.UpdateStatusObject(updateStatusObjectId);
        
        return objectiveId;
    }

    public async Task<Result<ObjectiveDTO>> Get(Guid id)
    {
        var objectiveEntity = await _objectiveRepository.Get(id);
        if (objectiveEntity == null)
        {
            return Result<ObjectiveDTO>.Failed($"Цель с идентификатором {id} не найдена");
        }
        
        var objective = _mapper.Map<ObjectiveDTO>(objectiveEntity);
        return Result<ObjectiveDTO>.Success(objective);
    }

    public async Task<Result<string>> GetCreatorId(Guid objectiveId)
    {
        var objectiveEntity = await _objectiveRepository.Get(objectiveId);
        if (objectiveEntity == null)
        {
            return Result<string>.Failed($"Цель с идентификатором {objectiveId} не найдена");
        }

        return Result<string>.Success(objectiveEntity.UserId);
    }

    public async Task<List<ObjectiveDTO>> GetAllForUser(string userId)
    {
        var objectiveEntities = await _objectiveRepository.GetAllForUser(userId);
        var objectives = _mapper.Map<List<ObjectiveDTO>>(objectiveEntities);
        return objectives;
    }

    public async Task<Result> Update(UpdateObjectiveDTO updatedObjective)
    {
        var rowsAffectedCount = await _objectiveRepository.Update(updatedObjective);
        return rowsAffectedCount == 0
            ? Result.Failed($"Цель с идентификатором {updatedObjective.ObjectiveId} не найдена")
            : Result.Success();
    }

    public async Task<Result> UpdateStatusObject(UpdateStatusObjectDTO updateStatusObjectDto)
    {
        var rowsAffectedCount = await _objectiveRepository.UpdateStatusObject(updateStatusObjectDto);
        return rowsAffectedCount == 0
            ? Result.Failed($"Цель с идентификатором {updateStatusObjectDto.ObjectiveId} не найдена")
            : Result.Success();
    }

    public async Task<Result> Delete(Guid id)
    {
        var rowsAffectedCount = await _objectiveRepository.Delete(id);
        return rowsAffectedCount == 0
            ? Result.Failed($"Цель с идентификатором {id} не найдена")
            : Result.Success();
    }
}
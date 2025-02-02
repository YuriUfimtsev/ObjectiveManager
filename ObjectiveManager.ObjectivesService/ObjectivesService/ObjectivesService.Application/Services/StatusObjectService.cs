using AutoMapper;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Application.Dto;
using ObjectivesService.Application.Services.Interfaces;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.Application.Services;

public class StatusObjectService : IStatusObjectService
{
    private readonly IStatusObjectRepository _statusObjectRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public StatusObjectService(IStatusObjectRepository statusObjectRepository,
        IMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _statusObjectRepository = statusObjectRepository;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> CreateObject(CreateStatusObjectDTO createStatusObjectDto)
    {
        var statusObjectEntity = new StatusObjectEntity
        {
            CreatedAt = _dateTimeProvider.Now(),
            ObjectiveId = createStatusObjectDto.ObjectiveId,
            StatusValueId = createStatusObjectDto.StatusValueId,
            Comment = createStatusObjectDto.Comment
        };

        var statusObjectId = await _statusObjectRepository.AddAsync(statusObjectEntity);
        return statusObjectId;
    }

    public async Task<List<StatusObjectDTO>> GetHistory(Guid objectiveId)
    {
        var entities =
            await _statusObjectRepository.GetAllWithStatusValues(objectiveId);
        var result = _mapper.Map<List<StatusObjectDTO>>(entities);
        
        return result;
    }
}
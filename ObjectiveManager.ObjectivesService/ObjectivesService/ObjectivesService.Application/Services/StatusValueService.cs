using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Application.Services.Interfaces;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.Application.Services;

public class StatusValueService : IStatusValueService
{
    private readonly IObjectiveStatusRepository _statusRepository;
    private readonly IMapper _mapper;

    public StatusValueService(IObjectiveStatusRepository statusRepository, IMapper mapper)
    {
        _statusRepository = statusRepository;
        _mapper = mapper;
    }

    public async Task<List<StatusValueDTO>> GetAll()
    {
        var entities = await _statusRepository.GetAll().ToListAsync();
        return _mapper.Map<List<StatusValueDTO>>(entities);
    }
}
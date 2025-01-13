using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Interfaces;

namespace ObjectiveManager.Application.Services;

public class StatusesService : IStatusesService
{
    private readonly IObjectiveStatusRepository _statusRepository;
    private readonly IMapper _mapper;

    public StatusesService(IObjectiveStatusRepository statusRepository, IMapper mapper)
    {
        _statusRepository = statusRepository;
        _mapper = mapper;
    }

    public async Task<List<ObjectiveStatus>> GetAll()
    {
        var entities = await _statusRepository.GetAll().ToListAsync();
        return _mapper.Map<List<ObjectiveStatus>>(entities);
    }
}
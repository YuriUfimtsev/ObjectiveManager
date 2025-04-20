using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotificationsService.Application.Services.Interfaces;
using NotificationsService.Domain.Interfaces;
using ObjectiveManager.Models.NotificationsService.DTO;

namespace NotificationsService.Application.Services;

public class FrequencyValuesService : IFrequencyValuesService
{
    private readonly IFrequencyValuesRepository _frequencyValuesRepository;
    private readonly IMapper _mapper;

    public FrequencyValuesService(IFrequencyValuesRepository frequencyValuesRepository, IMapper mapper)
    {
        _frequencyValuesRepository = frequencyValuesRepository;
        _mapper = mapper;
    }

    public async Task<List<FrequencyValueDTO>> GetAll()
    {
        var entities = await _frequencyValuesRepository.GetAll().ToListAsync();
        return _mapper.Map<List<FrequencyValueDTO>>(entities);
    }
}
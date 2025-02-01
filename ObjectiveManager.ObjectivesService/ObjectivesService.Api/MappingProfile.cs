using AutoMapper;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Application.Models.Dto;
using ObjectivesService.Domain.DTO;

namespace ObjectivesService.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ObjectivePutDto, UpdateObjectiveDTO>();
        CreateMap<ObjectivePutDto, CreateStatusObjectDTO>();
    }
}
using AutoMapper;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Application.Dto;
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
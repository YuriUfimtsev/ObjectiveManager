using AutoMapper;
using ObjectiveManager.Api.Dto;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Dto;

namespace ObjectiveManager.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ObjectivePostDto, CreateObjectiveDto>();
        CreateMap<ObjectiveUpdateDto, Objective>();
    }
}
using AutoMapper;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ObjectiveEntity, Objective>();
        CreateMap<ObjectiveStatusEntity, ObjectiveStatus>();
    }
}
using AutoMapper;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Application.Dto;
using ObjectivesService.Domain.Entities;

namespace ObjectivesService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ObjectiveEntity, ObjectiveDTO>();
        CreateMap<StatusValueEntity, StatusValueDTO>();
        CreateMap<StatusObjectEntity, StatusObjectDTO>();
        
        // Настриваем установку свойства CreatedAt в текущее время при маппинге в ObjectiveEntity
        CreateMap<CreateObjectiveDto, ObjectiveEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom<CreatedAtResolver>());
        
    }
}
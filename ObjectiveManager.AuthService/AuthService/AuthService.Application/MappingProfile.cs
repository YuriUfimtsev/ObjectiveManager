using AuthService.Domain.Entities;
using AutoMapper;
using ObjectiveManager.Models.AuthService.Dto;

namespace AuthService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDto, User>();
    }
}
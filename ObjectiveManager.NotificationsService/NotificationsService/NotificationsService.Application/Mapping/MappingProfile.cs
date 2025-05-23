﻿using AutoMapper;
using NotificationsService.Domain.Entities;
using ObjectiveManager.Models.NotificationsService.DTO;

namespace NotificationsService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FrequencyValueEntity, FrequencyValueDTO>();
        CreateMap<NotificationEntity, NotificationDTO>();
    }
}
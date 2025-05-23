﻿using ObjectiveManager.Models.NotificationsService.DTO;
using ObjectiveManager.Models.Result;

namespace NotificationsService.Application.Services.Interfaces;

public interface INotificationsService
{
    public Task<Guid> Create(string userId);
    
    public Task<Result<NotificationDTO>> GetForUser(string userId);
    
    public Task<Result> UpdateFrequency(Guid id, long frequencyValueId);
}
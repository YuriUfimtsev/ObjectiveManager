﻿using ObjectiveManager.Models.NotificationsService.DTO;
using ObjectiveManager.Models.Result;

namespace NotificationsService.Client;

public interface INotificationsServiceClient
{
    public Task<string> CreateNotification(string userId);
    public Task<Result<NotificationDTO>> GetNotification();
    public Task<Result> UpdateNotificationFrequency(string notificationId, long frequencyValueId);
    public Task<FrequencyValueDTO[]> GetAllFrequencyValues();
}
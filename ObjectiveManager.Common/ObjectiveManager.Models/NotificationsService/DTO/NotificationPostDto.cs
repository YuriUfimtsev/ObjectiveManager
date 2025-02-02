namespace ObjectiveManager.Models.NotificationsService.DTO;

public record NotificationPostDto(
    bool IsMentor,
    DateTimeOffset NextNotificationTime,
    long FrequencyValueId);
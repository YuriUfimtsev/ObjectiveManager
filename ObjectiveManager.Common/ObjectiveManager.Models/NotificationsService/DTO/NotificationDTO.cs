namespace ObjectiveManager.Models.NotificationsService.DTO;

public record NotificationDTO(
    Guid Id,
    DateTimeOffset NextNotificationTime,
    FrequencyValueDTO FrequencyValue);
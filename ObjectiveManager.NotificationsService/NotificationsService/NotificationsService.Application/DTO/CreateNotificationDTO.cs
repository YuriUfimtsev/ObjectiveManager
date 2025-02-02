namespace NotificationsService.Application.DTO;

public record CreateNotificationDTO(
    string UserId,
    bool IsMentor,
    DateTimeOffset NextNotificationTime,
    long FrequencyValueId
);

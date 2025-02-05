using ObjectiveManager.Models.EntityFramework;

namespace NotificationsService.Domain.Entities;

public record NotificationEntity : IEntity<Guid>
{
    public Guid Id { get; set; }
    
    public required string UserId { get; init; }
    
    public required DateTimeOffset NextNotificationTime { get; set; }
    
    public required long FrequencyValueId { get; set; }
    public FrequencyValueEntity? FrequencyValue { get; set; }
}
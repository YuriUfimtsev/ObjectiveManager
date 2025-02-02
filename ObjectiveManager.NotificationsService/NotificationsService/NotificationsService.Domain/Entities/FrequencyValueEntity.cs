using ObjectiveManager.Models.EntityFramework;

namespace NotificationsService.Domain.Entities;

public record FrequencyValueEntity : IEntity<long>
{
    public long Id { get; set; }
    
    public required string Name { get; init; }
    
    public required long IntervalInHours { get; init; }
}
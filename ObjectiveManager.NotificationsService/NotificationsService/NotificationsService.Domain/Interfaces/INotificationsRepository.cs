using NotificationsService.Domain.Entities;

namespace NotificationsService.Domain.Interfaces;

public interface INotificationsRepository
{
    public Task<Guid> Create(NotificationEntity notification);
    
    public Task<NotificationEntity?> Get(Guid id);
    
    public Task<NotificationEntity?> GetForUser(string userId);
    
    public Task<int> UpdateFrequency(Guid id, long frequencyValueId);
    
    public Task<int> UpdateNextNotificationTime(Guid id, DateTimeOffset nextNotificationTime);
}
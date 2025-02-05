using Microsoft.EntityFrameworkCore;
using NotificationsService.DataAccess.Models;
using NotificationsService.Domain.Entities;
using NotificationsService.Domain.Interfaces;
using ObjectiveManager.Models.EntityFramework;

namespace NotificationsService.DataAccess.Repositories;

public class NotificationsRepository : CrudRepository<Guid, NotificationEntity>,
    INotificationsRepository
{
    public NotificationsRepository(NotificationsContext context)
        : base(context)
    {
    }

    public async Task<Guid> Create(NotificationEntity notification)
        => await AddAsync(notification);

    public async Task<NotificationEntity?> Get(Guid id)
        => await Context.Set<NotificationEntity>()
            .Include(obj => obj.FrequencyValue)
            .FirstOrDefaultAsync(obj => obj.Id == id);
    
    public new async Task<List<NotificationEntity>> GetAll()
        => await Context.Set<NotificationEntity>()
            .Include(obj => obj.FrequencyValue)
            .ToListAsync();

    public async Task<NotificationEntity?> GetForUser(string userId)
        => await Context.Set<NotificationEntity>()
            .Include(obj => obj.FrequencyValue)
            .FirstOrDefaultAsync(obj => obj.UserId == userId);

    public async Task<int> UpdateFrequency(Guid id, long frequencyValueId)
        => await base.UpdateAsync(id,
            baseNotification => new NotificationEntity
            {
                UserId = baseNotification.UserId,
                NextNotificationTime = baseNotification.NextNotificationTime,

                FrequencyValueId = frequencyValueId
            });

    public async Task<int> UpdateNextNotificationTime(Guid id, DateTimeOffset nextNotificationTime)
        => await base.UpdateAsync(id,
            baseNotification => new NotificationEntity
            {
                UserId = baseNotification.UserId,
                FrequencyValueId = baseNotification.FrequencyValueId,

                NextNotificationTime = nextNotificationTime
            });
}
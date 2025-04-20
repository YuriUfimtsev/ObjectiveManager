using NotificationsService.DataAccess.Models;
using NotificationsService.Domain.Entities;
using NotificationsService.Domain.Interfaces;
using ObjectiveManager.Models.EntityFramework;

namespace NotificationsService.DataAccess.Repositories;

public class FrequencyValuesRepository : ReadOnlyRepository<long, FrequencyValueEntity>, IFrequencyValuesRepository
{
    public FrequencyValuesRepository(NotificationsContext context)
        : base(context)
    {
    }
}
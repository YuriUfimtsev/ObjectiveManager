using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.DataAccess.Infrastructure;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now()
        => DateTimeOffset.Now;
}
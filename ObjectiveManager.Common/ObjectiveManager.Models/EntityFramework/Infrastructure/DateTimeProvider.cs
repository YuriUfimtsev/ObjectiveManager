namespace ObjectiveManager.Models.EntityFramework.Infrastructure;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now()
        => DateTimeOffset.Now;
}
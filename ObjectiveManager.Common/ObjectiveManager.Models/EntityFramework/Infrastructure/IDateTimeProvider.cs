namespace ObjectiveManager.Models.EntityFramework.Infrastructure;

public interface IDateTimeProvider
{
    public DateTimeOffset Now();
}
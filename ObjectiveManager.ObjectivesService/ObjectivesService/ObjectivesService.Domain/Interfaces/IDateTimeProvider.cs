namespace ObjectivesService.Domain.Interfaces;

public interface IDateTimeProvider
{
    public DateTimeOffset Now();
}
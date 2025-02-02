using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ObjectiveManager.Models.EntityFramework.Infrastructure;

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter()
        : base(
            d => d.ToUniversalTime(),
            d => d)
    {
    }
}
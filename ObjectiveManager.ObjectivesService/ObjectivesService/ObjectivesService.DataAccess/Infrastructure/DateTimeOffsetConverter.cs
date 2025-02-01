using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ObjectivesService.DataAccess.Infrastructure;

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter()
        : base(
            d => d.ToUniversalTime(),
            d => d)
    {
    }
}
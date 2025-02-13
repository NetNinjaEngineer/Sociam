using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Sociam.Infrastructure.Persistence.Converters;
internal sealed class UtcDateTimeOffsetConverter()
    : ValueConverter<DateTimeOffset, DateTime>(dto => dto.UtcDateTime, dt => new DateTimeOffset(dt, TimeSpan.Zero));

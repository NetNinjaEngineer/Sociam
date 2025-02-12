namespace Sociam.Application.Helpers;
public static class DateTimeOffsetExtensions
{
    public static DateTime ConvertToUserLocalTimeZone(this DateTimeOffset utcDateTime, string? timeZoneId)
    {
        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId ?? "UTC");
        var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime.UtcDateTime, userTimeZone);
        return localTime;
    }
}

namespace Sociam.Application.Helpers;
public sealed class RateLimitingOptions
{
    public int MaxRequests { get; set; }
    public int TimeWindow { get; set; }
    public int BlockTimeInMinutes { get; set; }
}

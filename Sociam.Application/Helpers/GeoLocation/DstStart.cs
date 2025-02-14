namespace Sociam.Application.Helpers.GeoLocation;

public class DstStart
{
    public string UtcTime { get; set; } = null!;
    public string Duration { get; set; } = null!;
    public bool Gap { get; set; }
    public string DateTimeAfter { get; set; } = null!;
    public string DateTimeBefore { get; set; } = null!;
    public bool Overlap { get; set; }
}
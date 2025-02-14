namespace Sociam.Application.Helpers.GeoLocation;

public class TimeZone
{
    public string Name { get; set; } = null!;
    public int Offset { get; set; }
    public int OffsetWithDst { get; set; }
    public string CurrentTime { get; set; } = null!;
    public double CurrentTimeUnix { get; set; }
    public bool IsDst { get; set; }
    public int DstSavings { get; set; }
    public bool DstExists { get; set; }
    public DstStart DstStart { get; set; } = null!;
    public DstEnd DstEnd { get; set; } = null!;
}
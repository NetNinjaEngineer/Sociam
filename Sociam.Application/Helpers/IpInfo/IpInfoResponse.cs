namespace Sociam.Application.Helpers.IpInfo;

public class IpInfoResponse
{
    public string Ip { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Loc { get; set; } = null!;
    public string Org { get; set; } = null!;
    public string Timezone { get; set; } = null!;
}
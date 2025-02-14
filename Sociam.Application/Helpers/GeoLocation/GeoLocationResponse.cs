namespace Sociam.Application.Helpers.GeoLocation;

public class GeoLocationResponse
{
    public string Ip { get; set; } = null!;
    public string ContinentCode { get; set; } = null!;
    public string ContinentName { get; set; } = null!;
    public string CountryCode2 { get; set; } = null!;
    public string CountryCode3 { get; set; } = null!;
    public string CountryName { get; set; } = null!;
    public string CountryNameOfficial { get; set; } = null!;
    public string CountryCapital { get; set; } = null!;
    public string StateProv { get; set; } = null!;
    public string StateCode { get; set; } = null!;
    public string District { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Zipcode { get; set; } = null!;
    public string Latitude { get; set; } = null!;
    public string Longitude { get; set; } = null!;
    public bool IsEu { get; set; }
    public string CallingCode { get; set; } = null!;
    public string CountryTld { get; set; } = null!;
    public string Languages { get; set; } = null!;
    public string CountryFlag { get; set; } = null!;
    public string GeonameId { get; set; } = null!;
    public string Isp { get; set; } = null!;
    public string ConnectionType { get; set; } = null!;
    public string Organization { get; set; } = null!;
    public string CountryEmoji { get; set; } = null!;
    public Currency Currency { get; set; } = null!;
    public TimeZone TimeZone { get; set; } = null!;
}
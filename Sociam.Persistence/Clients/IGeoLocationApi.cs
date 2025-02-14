using Refit;
using Sociam.Application.Helpers.GeoLocation;

namespace Sociam.Persistence.Clients;
public interface IGeoLocationApi
{
    [Get("/ipgeo?apiKey={apiKey}&ip={ip}")]
    Task<GeoLocationResponse?> GetGeoLocationAsync(string apiKey, string ip);
}

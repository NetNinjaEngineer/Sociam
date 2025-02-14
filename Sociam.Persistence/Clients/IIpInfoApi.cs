using Refit;
using Sociam.Application.Helpers.IpInfo;

namespace Sociam.Persistence.Clients;
public interface IIpInfoApi
{
    [Get("/{ip}?token={accessToken}")]
    Task<IpInfoResponse?> GetIpInfoAsync(string ip, string accessToken);
}

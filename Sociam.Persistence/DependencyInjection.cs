using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Sociam.Application.Helpers.GeoLocation;
using Sociam.Application.Helpers.IpInfo;
using Sociam.Persistence.Clients;
using System.Net.Http.Headers;

namespace Sociam.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDependecies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var ipInfoOptions = new IpInfoOptions();
        configuration.GetSection(nameof(IpInfoOptions)).Bind(ipInfoOptions);

        var geoLocationOptions = new IpGeoLocationOptions();
        configuration.GetSection(nameof(IpGeoLocationOptions)).Bind(geoLocationOptions);

        services.Configure<IpGeoLocationOptions>(configuration.GetSection(nameof(IpGeoLocationOptions)));

        services.Configure<IpInfoOptions>(configuration.GetSection(nameof(IpInfoOptions)));

        services.AddRefitClient<IGeoLocationApi>()
            .ConfigureHttpClient(options =>
            {
                options.BaseAddress = new Uri(geoLocationOptions.BaseUrl);
                options.DefaultRequestHeaders.Clear();

                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        services.AddRefitClient<IIpInfoApi>()
            .ConfigureHttpClient(options =>
            {
                options.BaseAddress = new Uri(ipInfoOptions.BaseUrl);
                options.DefaultRequestHeaders.Clear();

                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


        return services;
    }
}

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Services.Services;

public sealed class CacheService(
    IMemoryCache memoryCache,
    ILogger<CacheService> logger) : ICacheService
{
    public T? Get<T>(string cacheKey) =>
        memoryCache.Get<T>(cacheKey) ?? default;

    public void Set(string cacheKey, object response, TimeSpan expirationTime)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(expirationTime)
            .SetPriority(CacheItemPriority.Normal);

        memoryCache.Set(cacheKey, response, cacheOptions);
    }

    public void Remove(string key) => memoryCache.Remove(key);

    public bool Exists(string key) => memoryCache.TryGetValue(key, out _);
}
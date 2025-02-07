namespace Sociam.Application.Interfaces.Services;

public interface ICacheService
{
    T? Get<T>(string cacheKey);
    void Set(string cacheKey, object response, TimeSpan expirationTime);
    void Remove(string key);
    bool Exists(string key);
}
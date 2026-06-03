using Microsoft.Extensions.Caching.Memory;

namespace CalorieTracker.Api.Services.MemoryCache;

public class MemoryCacheService : IMemoryCacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, TimeSpan ttl, Func<Task<T?>> repository)
    {
        if (_cache.TryGetValue(key, out T? value))
        {
            return value;
        }

        var result = await repository();

        if (result != null)
        {
            _cache.Set(key, result, ttl);
        }

        return result;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
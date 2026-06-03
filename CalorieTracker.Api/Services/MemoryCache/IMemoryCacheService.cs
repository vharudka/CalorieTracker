namespace CalorieTracker.Api.Services.MemoryCache;

public interface IMemoryCacheService
{
    Task<T?> GetOrCreateAsync<T>(string key, TimeSpan ttl, Func<Task<T?>> repository);
    void Remove(string key);
}
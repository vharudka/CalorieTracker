using CalorieTracker.Api.Dtos.UserGoals;
using CalorieTracker.Api.Helpers;
using CalorieTracker.Api.Repositories.UserGoals;
using CalorieTracker.Api.Services.MemoryCache;

namespace CalorieTracker.Api.Services.UserGoals;

public class UserGoalsService : IUserGoalsService
{
    private readonly IUserGoalsRepository _repository;
    private readonly IMemoryCacheService _cache;

    public UserGoalsService(IUserGoalsRepository repository, IMemoryCacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<UserGoalsResponse?> GetAsync(Guid userId)
    {
        var result = await _cache.GetOrCreateAsync
        (
            CacheKeys.UserGoals(userId),
            TimeSpan.FromMinutes(30),
            () => _repository.GetAsync(userId)
        );

        return result ?? throw new Exception("User goal entry not found");
    }

    public Task<UserGoalsResponse> UpsertAsync(Guid userId, SetUserGoalsRequest request)
    {
        var result = _repository.UpsertAsync(userId, request);

        _cache.Remove(CacheKeys.UserGoals(userId));

        return result;
    }
}
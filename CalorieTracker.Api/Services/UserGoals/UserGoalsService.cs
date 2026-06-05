using CalorieTracker.Api.Dtos.UserGoals;
using CalorieTracker.Api.Exceptions;
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
            TimeSpan.FromHours(24),
            () => _repository.GetAsync(userId)
        );

        return result ?? throw new UserGoalsNotFoundException(userId);
    }

    public Task<UserGoalsResponse> UpsertAsync(Guid userId, SetUserGoalsRequest request)
    {
        var result = _repository.UpsertAsync(userId, request);

        _cache.Remove(CacheKeys.UserGoals(userId));

        return result;
    }
}
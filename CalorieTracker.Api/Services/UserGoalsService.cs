using CalorieTracker.Api.Dtos.UserGoals;
using CalorieTracker.Api.Repositories;

namespace CalorieTracker.Api.Services;

public class UserGoalsService : IUserGoalsService
{
    private readonly IUserGoalsRepository _repository;

    public UserGoalsService(IUserGoalsRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserGoalsResponse?> GetAsync(Guid userId)
    {
        var entry = await _repository.GetAsync(userId);

        return entry is null ? throw new Exception("User goal entry not found") : entry;
    }

    public Task<UserGoalsResponse> UpsertAsync(Guid userId, SetUserGoalsRequest request)
        => _repository.UpsertAsync(userId, request);
}
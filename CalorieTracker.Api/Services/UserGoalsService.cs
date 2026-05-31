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

    public async Task<UserGoalsResponse> GetAsync()
    {
        return await _repository.GetAsync();
    }

    public async Task<UserGoalsResponse> SetAsync(SetUserGoalsRequest request)
    {
        return await _repository.SetAsync(request);
    }
}
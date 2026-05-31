using CalorieTracker.Api.Dtos.UserGoals;

namespace CalorieTracker.Api.Repositories;

public interface IUserGoalsRepository
{
    Task<UserGoalsResponse> GetAsync();
    Task<UserGoalsResponse> SetAsync(SetUserGoalsRequest request);
}
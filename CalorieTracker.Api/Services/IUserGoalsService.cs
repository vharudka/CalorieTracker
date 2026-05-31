using CalorieTracker.Api.Dtos.UserGoals;

namespace CalorieTracker.Api.Services;

public interface IUserGoalsService
{
    Task<UserGoalsResponse> GetAsync();
    Task<UserGoalsResponse> SetAsync(SetUserGoalsRequest request);
}
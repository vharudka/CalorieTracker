using CalorieTracker.Api.Dtos.UserGoals;

namespace CalorieTracker.Api.Services;

public interface IUserGoalsService
{
    Task<UserGoalsResponse?> GetAsync(Guid userId);
    Task<UserGoalsResponse> UpsertAsync(Guid userId, SetUserGoalsRequest request);
}
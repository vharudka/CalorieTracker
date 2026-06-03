using CalorieTracker.Api.Dtos.UserGoals;

namespace CalorieTracker.Api.Repositories.UserGoals;

public interface IUserGoalsRepository
{
    Task<UserGoalsResponse?> GetAsync(Guid userId);
    Task<UserGoalsResponse> UpsertAsync(Guid userId, SetUserGoalsRequest request);
}
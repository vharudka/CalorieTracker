using CalorieTracker.Api.Dtos.UserGoals;

namespace CalorieTracker.Api.Repositories;

public interface IUserGoalsRepository
{
    Task<UserGoalsResponse?> GetAsync(Guid userId);
    Task<UserGoalsResponse> UpsertAsync(Guid userId, SetUserGoalsRequest request);
}
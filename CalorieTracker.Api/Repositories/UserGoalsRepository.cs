using CalorieTracker.Api.Dtos.UserGoals;
using System.Data;

namespace CalorieTracker.Api.Repositories;

public class UserGoalsRepository : IUserGoalsRepository
{
    private readonly IDbConnection _db;

    public UserGoalsRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<UserGoalsResponse> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserGoalsResponse> SetAsync(SetUserGoalsRequest request)
    {
        throw new NotImplementedException();
    }
}
using CalorieTracker.Api.Dtos.UserGoals;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories;

public class UserGoalsRepository : IUserGoalsRepository
{
    private readonly IDbConnection _db;

    public UserGoalsRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<UserGoalsResponse?> GetAsync(Guid userId)
    {
        return await _db.QuerySingleOrDefaultAsync<UserGoalsResponse>
        (
            "spGetUserGoals",
            new { UserId = userId },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<UserGoalsResponse> UpsertAsync(Guid userId, SetUserGoalsRequest request)
    {
        return await _db.QuerySingleAsync<UserGoalsResponse>
        (
            "spUpsertUserGoals",
            new
            {
                UserId = userId,
                request.DailyCalorieLimit
            },
            commandType: CommandType.StoredProcedure
        );
    }
}
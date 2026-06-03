using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Models;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories.Stats;

public class StatsRepository : IStatsRepository
{
    private readonly IDbConnection _db;

    public StatsRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<DailyStats?> GetDailyAsync(Guid userId, DateTime date)
    {
        using var multi = await _db.QueryMultipleAsync
        (
            "spGetDailyStats",
            new
            {
                UserId = userId,
                Date = date.Date
            },
            commandType: CommandType.StoredProcedure
        );

        var total = await multi.ReadSingleOrDefaultAsync<int?>();
        if (total is null) return null;

        var limit = await multi.ReadSingleAsync<int>();
        var entries = (await multi.ReadAsync<FoodEntryResponse>()).ToList();

        return new DailyStats(total.Value, limit, entries);
    }

    public async Task<WeeklyStats?> GetWeeklyAsync(Guid userId, DateTime start, DateTime end)
    {
        using var multi = await _db.QueryMultipleAsync
        (
            "spGetWeeklyStats",
            new
            {
                UserId = userId,
                StartDate = start.Date,
                EndDate = end.Date
            },
            commandType: CommandType.StoredProcedure
        );

        var total = await multi.ReadSingleOrDefaultAsync<int?>();
        if (total is null)
        {
            return null;
        }

        var limit = await multi.ReadSingleAsync<int>();

        return new WeeklyStats(total.Value, limit);
    }

    public async Task<MonthlyStats?> GetMonthlyAsync(Guid userId, int year, int month)
    {
        using var multi = await _db.QueryMultipleAsync
        (
            "spGetMonthlyStats",
            new
            {
                UserId = userId,
                Year = year,
                Month = month
            },
            commandType: CommandType.StoredProcedure
        );

        var total = await multi.ReadSingleOrDefaultAsync<int?>();
        if (total is null)
        {
            return null;
        }

        var limit = await multi.ReadSingleAsync<int>();

        return new MonthlyStats(total.Value, limit);
    }
}
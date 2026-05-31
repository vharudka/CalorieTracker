using CalorieTracker.Api.Dtos.Stats;
using System.Data;

namespace CalorieTracker.Api.Repositories;

public class StatsRepository : IStatsRepository
{
    private readonly IDbConnection _db;

    public StatsRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<DailyStatsResponse> GetDailyAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public async Task<WeeklyStatsResponse> GetWeeklyAsync(DateTime weekStart)
    {
        throw new NotImplementedException();
    }

    public async Task<MonthlyStatsResponse> GetMonthlyAsync(int year, int month)
    {
        throw new NotImplementedException();
    }
}
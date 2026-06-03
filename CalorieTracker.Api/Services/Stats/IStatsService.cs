using CalorieTracker.Api.Dtos.Stats;

namespace CalorieTracker.Api.Services.Stats;

public interface IStatsService
{
    Task<DailyStatsResponse> GetDailyAsync(Guid userId, DailyStatsRequest request);
    Task<WeeklyStatsResponse> GetWeeklyAsync(Guid userId, WeeklyStatsRequest request);
    Task<MonthlyStatsResponse> GetMonthlyAsync(Guid userId, MonthlyStatsRequest request);
}
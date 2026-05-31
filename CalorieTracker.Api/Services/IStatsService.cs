using CalorieTracker.Api.Dtos.Stats;

namespace CalorieTracker.Api.Services;

public interface IStatsService
{
    Task<DailyStatsResponse> GetDailyAsync(DateTime date);
    Task<WeeklyStatsResponse> GetWeeklyAsync(DateTime weekStart);
    Task<MonthlyStatsResponse> GetMonthlyAsync(int year, int month);
}
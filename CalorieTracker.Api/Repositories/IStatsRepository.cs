using CalorieTracker.Api.Dtos.Stats;

namespace CalorieTracker.Api.Repositories;

public interface IStatsRepository
{
    Task<DailyStatsResponse> GetDailyAsync(DateTime date);
    Task<WeeklyStatsResponse> GetWeeklyAsync(DateTime weekStart);
    Task<MonthlyStatsResponse> GetMonthlyAsync(int year, int month);
}
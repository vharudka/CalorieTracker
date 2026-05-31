using CalorieTracker.Api.Dtos.Stats;

namespace CalorieTracker.Api.Services;

public interface IStatsService
{
    Task<DailyStatsResponse> GetDailyAsync(Guid userId, DateTime date);
    Task<WeeklyStatsResponse> GetWeeklyAsync(Guid userId, DateTime date);
    Task<MonthlyStatsResponse> GetMonthlyAsync(Guid userId, int year, int month);
}
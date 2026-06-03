using CalorieTracker.Api.Models;

namespace CalorieTracker.Api.Repositories.Stats;

public interface IStatsRepository
{
    Task<DailyStats?> GetDailyAsync(Guid userId, DateTime date);
    Task<WeeklyStats?> GetWeeklyAsync(Guid userId, DateTime start, DateTime end);
    Task<MonthlyStats?> GetMonthlyAsync(Guid userId, int year, int month);
}
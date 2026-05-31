using CalorieTracker.Api.Dtos.Stats;

namespace CalorieTracker.Api.Services;

public class StatsService : IStatsService
{
    private readonly IStatsRepository _repository;

    public StatsService(IStatsRepository repository)
    {
        _repository = repository;
    }

    public async Task<DailyStatsResponse> GetDailyAsync(DateTime date)
    {
        return await _repository.GetDailyAsync(date);
    }

    public async Task<WeeklyStatsResponse> GetWeeklyAsync(DateTime weekStart)
    {
        return await _repository.GetWeeklyAsync(weekStart);
    }

    public async Task<MonthlyStatsResponse> GetMonthlyAsync(int year, int month)
    {
        return await _repository.GetMonthlyAsync(year, month);
    }
}
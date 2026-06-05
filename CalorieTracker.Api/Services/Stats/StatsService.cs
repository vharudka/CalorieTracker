using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Exceptions;
using CalorieTracker.Api.Repositories.Stats;

namespace CalorieTracker.Api.Services.Stats;

public class StatsService : IStatsService
{
    private readonly IStatsRepository _repository;

    public StatsService(IStatsRepository repository)
    {
        _repository = repository;
    }

    public async Task<DailyStatsResponse> GetDailyAsync(Guid userId, DailyStatsRequest request)
    {
        var date = request.Date;
        var raw = await _repository.GetDailyAsync(userId, date)
            ?? throw new UserGoalsNotFoundException(userId);

        var remaining = raw.DailyCalorieLimit - raw.TotalCalories;

        return new DailyStatsResponse
        (
            Date: date.Date,
            TotalCalories: raw.TotalCalories,
            DailyCalorieLimit: raw.DailyCalorieLimit,
            RemainingCalories: remaining,
            Entries: raw.Entries
        );
    }

    public async Task<WeeklyStatsResponse> GetWeeklyAsync(Guid userId, WeeklyStatsRequest request)
    {
        var date = request.Date;
        var weekStart = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
        var weekEnd = weekStart.AddDays(6);

        var raw = await _repository.GetWeeklyAsync(userId, weekStart, weekEnd)
            ?? throw new UserGoalsNotFoundException(userId);

        var days = 7;

        return new WeeklyStatsResponse
        (
            WeekStart: weekStart,
            WeekEnd: weekEnd,
            TotalCalories: raw.TotalCalories,
            AverageCalories: raw.TotalCalories / days,
            DailyCalorieLimit: raw.DailyCalorieLimit,
            RemainingCalories: raw.DailyCalorieLimit * days - raw.TotalCalories
        );
    }

    public async Task<MonthlyStatsResponse> GetMonthlyAsync(Guid userId, MonthlyStatsRequest request)
    {
        var year = request.Year;
        var month = request.Month;

        var raw = await _repository.GetMonthlyAsync(userId, year, month)
            ?? throw new UserGoalsNotFoundException(userId);

        var days = DateTime.DaysInMonth(year, month);

        return new MonthlyStatsResponse
        (
            Year: year,
            Month: month,
            TotalCalories: raw.TotalCalories,
            AverageCalories: raw.TotalCalories / days,
            DailyCalorieLimit: raw.DailyCalorieLimit,
            RemainingCalories: raw.DailyCalorieLimit * days - raw.TotalCalories
        );
    }
}
namespace CalorieTracker.Api.Dtos.Stats;

public record MonthlyStatsResponse
(
    int Year,
    int Month,
    int TotalCalories,
    int AverageCalories,
    int DailyCalorieLimit,
    int RemainingCalories
);
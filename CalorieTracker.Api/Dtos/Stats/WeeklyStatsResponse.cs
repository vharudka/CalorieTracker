namespace CalorieTracker.Api.Dtos.Stats;

public record WeeklyStatsResponse
(
    DateTime WeekStart,
    DateTime WeekEnd,
    int TotalCalories,
    int AverageCalories,
    int DailyCalorieLimit,
    int RemainingCalories,
    IReadOnlyList<int> DailyCalories,
    int TotalProtein,
    int TotalFat,
    int TotalCarbohydrates
);
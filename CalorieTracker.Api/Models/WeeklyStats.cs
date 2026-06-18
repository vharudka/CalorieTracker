namespace CalorieTracker.Api.Models;

public record WeeklyStats
(
    int TotalCalories,
    int DailyCalorieLimit,
    IReadOnlyList<int> DailyCalories,
    int TotalProtein,
    int TotalFat,
    int TotalCarbohydrates
);
namespace CalorieTracker.Api.Models;

public record MonthlyStats
(
    int TotalCalories,
    int DailyCalorieLimit,
    IReadOnlyList<int> DailyCalories,
    int TotalProtein,
    int TotalFat,
    int TotalCarbohydrates
);
using CalorieTracker.Api.Dtos.FoodEntries;

namespace CalorieTracker.Api.Models;

public record DailyStats
(
    int TotalCalories,
    int DailyCalorieLimit,
    IReadOnlyList<FoodEntryResponse> Entries
);
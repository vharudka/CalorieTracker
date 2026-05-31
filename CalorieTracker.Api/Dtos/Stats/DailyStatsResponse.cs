using CalorieTracker.Api.Dtos.FoodEntries;

namespace CalorieTracker.Api.Dtos.Stats;

public record DailyStatsResponse
(
    DateTime Date,
    int TotalCalories,
    int DailyCalorieLimit,
    int RemainingCalories,
    IReadOnlyList<FoodEntryResponse> Entries
);
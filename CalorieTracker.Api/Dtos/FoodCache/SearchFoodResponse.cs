namespace CalorieTracker.Api.Dtos.FoodCache;

public record SearchFoodResponse
(
    IReadOnlyList<FoodCacheResponse> Items
);
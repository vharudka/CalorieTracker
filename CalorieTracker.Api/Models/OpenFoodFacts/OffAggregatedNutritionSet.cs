namespace CalorieTracker.Api.Models.OpenFoodFacts;

public record OffAggregatedNutritionSet
(
    Dictionary<string, OffNutrient> Nutrients
);
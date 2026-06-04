using System.Text.Json.Serialization;

namespace CalorieTracker.Api.Models.OpenFoodFacts;

public record OffNutrition
(
     [property: JsonPropertyName("aggregated_set")] OffAggregatedNutritionSet AggregatedSet
);
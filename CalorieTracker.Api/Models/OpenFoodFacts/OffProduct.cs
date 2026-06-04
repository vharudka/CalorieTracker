using System.Text.Json.Serialization;

namespace CalorieTracker.Api.Models.OpenFoodFacts;

public record OffProduct
(
    [property: JsonPropertyName("product_name")] string ProductName,
    OffNutrition Nutrition
);
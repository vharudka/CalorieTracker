namespace CalorieTracker.Api.Models.OpenFoodFacts;

public record OffProduct
(
    string ProductName,
    OffNutrition Nutrition
);
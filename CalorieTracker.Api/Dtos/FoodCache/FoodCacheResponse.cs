namespace CalorieTracker.Api.Dtos.FoodCache;

public record FoodCacheResponse
(
    string Name,
    string Barcode,
    decimal Calories,
    decimal Protein,
    decimal Fat,
    decimal Carbohydrates,
    DateTime UpdatedAt
);
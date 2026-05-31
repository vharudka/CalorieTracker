namespace CalorieTracker.Api.Dtos.FoodCache;

public record FoodCacheResponse
(
    Guid Id,
    string FoodName,
    string? Barcode,
    int Calories,
    decimal Protein,
    decimal Fat,
    decimal Carbohydrates
);
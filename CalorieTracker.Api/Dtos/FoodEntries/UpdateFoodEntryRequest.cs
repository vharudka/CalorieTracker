namespace CalorieTracker.Api.Dtos.FoodEntries;

public record UpdateFoodEntryRequest
(
    string FoodName,
    string? Barcode,
    int Calories,
    decimal Protein,
    decimal Fat,
    decimal Carbohydrates,
    DateTime EatenAt
);

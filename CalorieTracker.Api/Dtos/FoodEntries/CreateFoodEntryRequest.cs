namespace CalorieTracker.Api.Dtos.FoodEntries;

public record CreateFoodEntryRequest
(
    string Barcode,
    decimal Grams,
    DateTime EatenAt
);
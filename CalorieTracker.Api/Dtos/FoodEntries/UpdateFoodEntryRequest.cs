namespace CalorieTracker.Api.Dtos.FoodEntries;

public record UpdateFoodEntryRequest
(
    string Barcode,
    int Grams,
    DateTime EatenAt
);
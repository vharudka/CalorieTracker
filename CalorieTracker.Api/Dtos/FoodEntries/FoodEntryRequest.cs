namespace CalorieTracker.Api.Dtos.FoodEntries;

public record FoodEntryRequest
(
    string Barcode,
    decimal Grams,
    DateTime EatenAt
);
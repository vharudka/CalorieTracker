namespace CalorieTracker.Api.Dtos.FoodEntries;

public record UpdateFoodEntryRequest
(
    string Barcode,
    decimal Grams,
    DateTime EatenAt
) : FoodEntryRequest(Barcode, Grams, EatenAt);
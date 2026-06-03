namespace CalorieTracker.Api.Models;

public record FoodEntry
(
    Guid Id,
    Guid UserId,
    string Name,
    string Barcode,
    decimal Grams,
    decimal Calories,
    decimal Protein,
    decimal Fat,
    decimal Carbohydrates,
    DateTime EatenAt
);
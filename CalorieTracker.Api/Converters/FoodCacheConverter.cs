using CalorieTracker.Api.Dtos.FoodCache;
using CalorieTracker.Api.Models.OpenFoodFacts;

namespace CalorieTracker.Api.Converters;

public static class FoodCacheConverter
{
    public static FoodCacheResponse ToFoodCacheResponse(this OffProductResponse offProductResponse)
    {
        var nutrients = offProductResponse.Product?
                                          .Nutrition?
                                          .AggregatedSet?
                                          .Nutrients;

        return new FoodCacheResponse
        (
            offProductResponse.Product?.ProductName ?? "Unknown",
            offProductResponse.Code,
            GetNutrient(nutrients, "energy-kcal"),
            GetNutrient(nutrients, "proteins"),
            GetNutrient(nutrients, "fat"),
            GetNutrient(nutrients, "carbohydrates"),
            DateTime.UtcNow
        );
    }

    private static decimal GetNutrient
    (
        Dictionary<string, OffNutrient>? nutrients,
        string key
    )
    {
        if (nutrients != null && nutrients.TryGetValue(key, out var nutrient))
        {
            return nutrient.Value;
        }

        return 0;
    }
}
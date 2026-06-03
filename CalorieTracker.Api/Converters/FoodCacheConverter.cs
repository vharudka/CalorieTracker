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
            nutrients?["energy-kcal"]?.Value ?? 0,
            nutrients?["proteins"]?.Value ?? 0,
            nutrients?["fat"]?.Value ?? 0,
            nutrients?["carbohydrates"]?.Value ?? 0,
            DateTime.UtcNow
        );
    }
}
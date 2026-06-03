using CalorieTracker.Api.Dtos.FoodCache;

namespace CalorieTracker.Api.Services.FoodCache;

public interface IFoodCacheService
{
    Task<FoodCacheResponse?> GetAsync(string barcode);
}
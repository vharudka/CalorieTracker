using CalorieTracker.Api.Dtos.FoodCache;

namespace CalorieTracker.Api.Repositories.FoodCache;

public interface IFoodCacheRepository
{
    Task<FoodCacheResponse?> GetAsync(string barcode);
    Task<FoodCacheResponse> InsertAsync(FoodCacheResponse product);
}
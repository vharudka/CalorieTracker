using CalorieTracker.Api.Dtos.FoodCache;

namespace CalorieTracker.Api.Services;

public interface IFoodCacheService
{
    Task<SearchFoodResponse> SearchAsync(string query);
}
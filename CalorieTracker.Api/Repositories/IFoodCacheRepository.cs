using CalorieTracker.Api.Dtos.FoodCache;

namespace CalorieTracker.Api.Repositories;

public interface IFoodCacheRepository
{
    Task<IReadOnlyList<FoodCacheResponse>> SearchAsync(string query);
}
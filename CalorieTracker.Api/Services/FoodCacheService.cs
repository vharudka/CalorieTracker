using CalorieTracker.Api.Dtos.FoodCache;

namespace CalorieTracker.Api.Services;

public class FoodCacheService : IFoodCacheService
{
    private readonly IFoodCacheRepository _repository;

    public FoodCacheService(IFoodCacheRepository repository)
    {
        _repository = repository;
    }

    public async Task<SearchFoodResponse> SearchAsync(string query)
    {
        var items = await _repository.SearchAsync(query);
        return new SearchFoodResponse(items);
    }
}
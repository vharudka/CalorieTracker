using CalorieTracker.Api.Converters;
using CalorieTracker.Api.Dtos.FoodCache;
using CalorieTracker.Api.Helpers;
using CalorieTracker.Api.Repositories.FoodCache;
using CalorieTracker.Api.Repositories.OpenFoodFacts;
using CalorieTracker.Api.Services.MemoryCache;

namespace CalorieTracker.Api.Services.FoodCache;

public class FoodCacheService : IFoodCacheService
{
    private readonly IMemoryCacheService _cache;
    private readonly IFoodCacheRepository _foodCacheRepository;
    private readonly IOpenFoodFactsRepository _openFoodFactsRepository;

    public FoodCacheService
    (
        IMemoryCacheService cache,
        IFoodCacheRepository foodCacheRepository,
        IOpenFoodFactsRepository openFoodFactsRepository
    )
    {
        _cache = cache;
        _foodCacheRepository = foodCacheRepository;
        _openFoodFactsRepository = openFoodFactsRepository;
    }

    public Task<FoodCacheResponse?> GetAsync(string barcode)
    {
        return _cache.GetOrCreateAsync
        (
            CacheKeys.FoodCacheKey(barcode),
            TimeSpan.FromHours(24),
            async () =>
            {
                var productFromDb = await _foodCacheRepository.GetAsync(barcode);
                if (productFromDb != null)
                {
                    return productFromDb;
                }

                var productFromApi = await _openFoodFactsRepository.GetAsync(barcode);
                if (productFromApi == null)
                {
                    return null;
                }

                var foodCacheResponse = productFromApi.ToFoodCacheResponse();
                var saved = await _foodCacheRepository.InsertAsync(foodCacheResponse);

                return saved;
            }
        );
    }
}
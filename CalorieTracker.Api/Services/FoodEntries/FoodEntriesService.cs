using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Models;
using CalorieTracker.Api.Repositories.FoodEntries;
using CalorieTracker.Api.Services.FoodCache;

namespace CalorieTracker.Api.Services.FoodEntries;

public class FoodEntriesService : IFoodEntriesService
{
    private readonly IFoodEntriesRepository _repository;
    private readonly IFoodCacheService _foodCache;

    public FoodEntriesService(IFoodEntriesRepository repository, IFoodCacheService foodCache)
    {
        _repository = repository;
        _foodCache = foodCache;
    }

    public async Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request, Guid userId)
    {
        var foodCache = await _foodCache.GetAsync(request.Barcode) ?? throw new Exception("Food cache not found");

        var factor = request.Grams / 100m;

        var foodEntry = new FoodEntry
        (
            Guid.NewGuid(),
            userId,
            foodCache.Name,
            foodCache.Barcode,
            request.Grams,
            foodCache.Calories * factor,
            foodCache.Protein * factor,
            foodCache.Fat * factor,
            foodCache.Carbohydrates * factor,
            request.EatenAt
        );

        return await _repository.CreateAsync(foodEntry);
    }

    public async Task<FoodEntryResponse> UpdateAsync(Guid id, Guid userId, UpdateFoodEntryRequest request)
    {
        var foodCache = await _foodCache.GetAsync(request.Barcode) ?? throw new Exception("Food cache not found");

        var factor = request.Grams / 100m;

        var foodEntry = new FoodEntry
        (
            Guid.NewGuid(),
            userId,
            foodCache.Name,
            foodCache.Barcode,
            request.Grams,
            foodCache.Calories * factor,
            foodCache.Protein * factor,
            foodCache.Fat * factor,
            foodCache.Carbohydrates * factor,
            request.EatenAt
        );

        return await _repository.UpdateAsync(foodEntry);
    }

    public async Task<FoodEntryResponse> GetAsync(Guid id, Guid userId)
    {
        var entry = await _repository.GetAsync(id, userId);

        return entry ?? throw new Exception("Food entry not found");
    }

    public Task<IReadOnlyList<FoodEntryResponse>> GetAllByUserAsync(Guid userId)
        => _repository.GetAllByUserAsync(userId);

    public Task DeleteAsync(Guid id, Guid userId)
        => _repository.DeleteAsync(id, userId);
}
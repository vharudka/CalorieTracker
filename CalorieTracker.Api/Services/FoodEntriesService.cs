using CalorieTracker.Api.Dtos.FoodEntries;

namespace CalorieTracker.Api.Services;

public class FoodEntriesService : IFoodEntriesService
{
    private readonly IFoodEntriesRepository _repository;

    public FoodEntriesService(IFoodEntriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request)
    {
        var entry = await _repository.CreateAsync(request);
        return entry;
    }

    public async Task<FoodEntryResponse> UpdateAsync(Guid id, UpdateFoodEntryRequest request)
    {
        var entry = await _repository.UpdateAsync(id, request);
        return entry;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
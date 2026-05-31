using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Repositories;

namespace CalorieTracker.Api.Services;

public class FoodEntriesService : IFoodEntriesService
{
    private readonly IFoodEntriesRepository _repository;

    public FoodEntriesService(IFoodEntriesRepository repository)
    {
        _repository = repository;
    }

    public Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request, Guid userId)
        => _repository.CreateAsync(request, userId);

    public async Task<FoodEntryResponse> UpdateAsync(Guid id, Guid userId, UpdateFoodEntryRequest request)
    {
        var updated = await _repository.UpdateAsync(id, userId, request);

        return updated is null ? throw new Exception("Food entry not found") : updated;
    }

    public async Task<FoodEntryResponse> GetAsync(Guid id, Guid userId)
    {
        var entry = await _repository.GetAsync(id, userId);

        return entry is null ? throw new Exception("Food entry not found") : entry;
    }

    public Task<IReadOnlyList<FoodEntryResponse>> GetAllByUserAsync(Guid userId)
        => _repository.GetAllByUserAsync(userId);

    public Task DeleteAsync(Guid id, Guid userId)
        => _repository.DeleteAsync(id, userId);
}
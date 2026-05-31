using CalorieTracker.Api.Dtos.FoodEntries;

namespace CalorieTracker.Api.Services;

public interface IFoodEntriesService
{
    Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request, Guid userId);
    Task<FoodEntryResponse> UpdateAsync(Guid id, Guid userId, UpdateFoodEntryRequest request);
    Task<FoodEntryResponse> GetAsync(Guid id, Guid userId);
    Task<IReadOnlyList<FoodEntryResponse>> GetAllByUserAsync(Guid userId);
    Task DeleteAsync(Guid id, Guid userId);
}
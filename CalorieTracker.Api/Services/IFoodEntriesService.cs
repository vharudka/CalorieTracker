using CalorieTracker.Api.Dtos.FoodEntries;

namespace CalorieTracker.Api.Services;

public interface IFoodEntriesService
{
    Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request);
    Task<FoodEntryResponse> UpdateAsync(Guid id, UpdateFoodEntryRequest request);
    Task DeleteAsync(Guid id);
}
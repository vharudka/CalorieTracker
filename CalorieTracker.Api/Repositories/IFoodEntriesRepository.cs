using CalorieTracker.Api.Dtos.FoodEntries;

namespace CalorieTracker.Api.Repositories;

public interface IFoodEntriesRepository
{
    Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request);
    Task<FoodEntryResponse> UpdateAsync(Guid id, UpdateFoodEntryRequest request);
    Task DeleteAsync(Guid id);
}
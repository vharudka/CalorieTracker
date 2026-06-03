using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Models;

namespace CalorieTracker.Api.Repositories.FoodEntries;

public interface IFoodEntriesRepository
{
    Task<FoodEntryResponse> CreateAsync(FoodEntry foodEntry);
    Task<FoodEntryResponse> UpdateAsync(FoodEntry foodEntry);
    Task<FoodEntryResponse?> GetAsync(Guid id, Guid userId);
    Task<IReadOnlyList<FoodEntryResponse>> GetAllByUserAsync(Guid userId);
    Task DeleteAsync(Guid id, Guid userId);
}
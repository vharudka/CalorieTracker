using CalorieTracker.Api.Dtos.FoodEntries;
using System.Data;

namespace CalorieTracker.Api.Repositories;

public class FoodEntriesRepository : IFoodEntriesRepository
{
    private readonly IDbConnection _db;

    public FoodEntriesRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<FoodEntryResponse> UpdateAsync(Guid id, UpdateFoodEntryRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
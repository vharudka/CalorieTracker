using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Models;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories.FoodEntries;

public class FoodEntriesRepository : IFoodEntriesRepository
{
    private readonly IDbConnection _db;

    public FoodEntriesRepository(IDbConnection db)
    {
        _db = db;
    }

    public Task<FoodEntryResponse> CreateAsync(FoodEntry foodEntry)
    {
        return _db.QuerySingleAsync<FoodEntryResponse>
        (
            "spCreateFoodEntry",
            new
            {
                foodEntry.Id,
                foodEntry.UserId,
                foodEntry.Name,
                foodEntry.Barcode,
                foodEntry.Grams,
                foodEntry.Calories,
                foodEntry.Protein,
                foodEntry.Fat,
                foodEntry.Carbohydrates,
                foodEntry.EatenAt
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public Task<FoodEntryResponse> UpdateAsync(FoodEntry foodEntry)
    {
        return _db.QuerySingleAsync<FoodEntryResponse>
        (
            "spUpdateFoodEntry",
            new
            {
                foodEntry.Id,
                foodEntry.UserId,
                foodEntry.Name,
                foodEntry.Barcode,
                foodEntry.Grams,
                foodEntry.Calories,
                foodEntry.Protein,
                foodEntry.Fat,
                foodEntry.Carbohydrates,
                foodEntry.EatenAt
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public Task<FoodEntryResponse?> GetAsync(Guid id, Guid userId)
    {
        return _db.QuerySingleOrDefaultAsync<FoodEntryResponse>
        (
            "spGetFoodEntry",
            new
            {
                Id = id,
                UserId = userId
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IReadOnlyList<FoodEntryResponse>> GetAllByUserAsync(Guid userId)
    {
        var result = await _db.QueryAsync<FoodEntryResponse>
        (
            "spGetFoodEntriesByUser",
            new { UserId = userId },
            commandType: CommandType.StoredProcedure
        );

        return result.ToList();
    }

    public Task DeleteAsync(Guid id, Guid userId)
    {
        return _db.ExecuteAsync
        (
            "spDeleteFoodEntry",
            new
            {
                Id = id,
                UserId = userId
            },
            commandType: CommandType.StoredProcedure
        );
    }
}
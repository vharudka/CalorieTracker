using CalorieTracker.Api.Dtos.FoodEntries;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories;

public class FoodEntriesRepository : IFoodEntriesRepository
{
    private readonly IDbConnection _db;

    public FoodEntriesRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<FoodEntryResponse> CreateAsync(CreateFoodEntryRequest request, Guid userId)
    {
        var id = Guid.NewGuid();

        return await _db.QuerySingleAsync<FoodEntryResponse>
        (
            "spCreateFoodEntry",
            new
            {
                Id = id,
                UserId = userId,
                request.FoodName,
                request.Barcode,
                request.Calories,
                request.Protein,
                request.Fat,
                request.Carbohydrates,
                request.EatenAt
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<FoodEntryResponse?> UpdateAsync(Guid id, Guid userId, UpdateFoodEntryRequest request)
    {
        return await _db.QuerySingleOrDefaultAsync<FoodEntryResponse>
        (
            "spUpdateFoodEntry",
            new
            {
                Id = id,
                UserId = userId,
                request.FoodName,
                request.Barcode,
                request.Calories,
                request.Protein,
                request.Fat,
                request.Carbohydrates,
                request.EatenAt
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<FoodEntryResponse?> GetAsync(Guid id, Guid userId)
    {
        return await _db.QuerySingleOrDefaultAsync<FoodEntryResponse>
        (
            "spGetFoodEntry",
            new { Id = id, UserId = userId },
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

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        await _db.ExecuteAsync
        (
            "spDeleteFoodEntry",
            new { Id = id, UserId = userId },
            commandType: CommandType.StoredProcedure
        );
    }
}
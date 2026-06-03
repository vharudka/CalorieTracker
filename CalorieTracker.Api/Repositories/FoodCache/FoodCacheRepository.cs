using CalorieTracker.Api.Dtos.FoodCache;
using Dapper;
using System.Data;

namespace CalorieTracker.Api.Repositories.FoodCache;

public class FoodCacheRepository : IFoodCacheRepository
{
    private readonly IDbConnection _db;

    public FoodCacheRepository(IDbConnection db)
    {
        _db = db;
    }

    public Task<FoodCacheResponse?> GetAsync(string barcode)
    {
        return _db.QuerySingleOrDefaultAsync<FoodCacheResponse>
        (
            "spGetFoodCacheByBarcode",
            new { Barcode = barcode },
            commandType: CommandType.StoredProcedure
        );
    }

    public Task<FoodCacheResponse> InsertAsync(FoodCacheResponse product)
    {
        return _db.QuerySingleAsync<FoodCacheResponse>
        (
            "spInsertFoodCache",
            new
            {
                product.Name,
                product.Barcode,
                product.Calories,
                product.Protein,
                product.Fat,
                product.Carbohydrates,
                product.UpdatedAt

            },
            commandType: CommandType.StoredProcedure
        );
    }
}
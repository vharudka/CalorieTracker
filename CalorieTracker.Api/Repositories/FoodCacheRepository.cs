using CalorieTracker.Api.Dtos.FoodCache;
using System.Data;

namespace CalorieTracker.Api.Repositories;

public class FoodCacheRepository : IFoodCacheRepository
{
    private readonly IDbConnection _db;

    public FoodCacheRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<FoodCacheResponse>> SearchAsync(string query)
    {
        throw new NotImplementedException();
    }
}
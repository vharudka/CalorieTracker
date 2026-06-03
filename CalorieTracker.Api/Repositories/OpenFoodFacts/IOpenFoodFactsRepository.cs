using CalorieTracker.Api.Models.OpenFoodFacts;

namespace CalorieTracker.Api.Repositories.OpenFoodFacts;

public interface IOpenFoodFactsRepository
{
    Task<OffProductResponse?> GetAsync(string barcode, CancellationToken ct = default);
}

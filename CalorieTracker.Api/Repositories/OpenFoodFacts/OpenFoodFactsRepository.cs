using CalorieTracker.Api.Models.OpenFoodFacts;

namespace CalorieTracker.Api.Repositories.OpenFoodFacts;

public class OpenFoodFactsRepository : IOpenFoodFactsRepository
{
    private readonly HttpClient _http;

    public OpenFoodFactsRepository(HttpClient http)
    {
        _http = http;
    }

    public async Task<OffProductResponse?> GetAsync(string barcode, CancellationToken ct = default)
    {
        var url = $"https://world.openfoodfacts.net/api/v3.6/product/{barcode}?fields=product_name,nutrition";
        return await _http.GetFromJsonAsync<OffProductResponse>(url, ct);
    }
}
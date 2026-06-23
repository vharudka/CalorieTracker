using CalorieTracker.Api.Models.OpenFoodFacts;
using CalorieTracker.Api.Converters;

namespace CalorieTracker.Api.Tests.Converters;

[TestClass]
public class FoodCacheConverterTests
{
    [TestMethod]
    public void ToFoodCacheResponse_WhenAllDataPresent_MapsCorrectly()
    {
        var nutrients = new Dictionary<string, OffNutrient>
            {
                { "energy-kcal", new OffNutrient(100, "unit") },
                { "proteins", new OffNutrient(10, "unit") },
                { "fat", new OffNutrient(5, "unit") },
                { "carbohydrates", new OffNutrient(20, "unit") }
            };

        var response = new OffProductResponse(
            Code: "123456",
            Product: new OffProduct(
                ProductName: "Test Product",
                Nutrition: new OffNutrition(
                    AggregatedSet: new OffAggregatedNutritionSet(nutrients)
                )
            )
        );

        var result = response.ToFoodCacheResponse();

        Assert.AreEqual("Test Product", result.Name);
        Assert.AreEqual("123456", result.Barcode);
        Assert.AreEqual(100, result.Calories);
        Assert.AreEqual(10, result.Protein);
        Assert.AreEqual(5, result.Fat);
        Assert.AreEqual(20, result.Carbohydrates);
    }

    [TestMethod]
    public void ToFoodCacheResponse_WhenNutrientsMissing_UsesZero()
    {
        var response = new OffProductResponse(
            Code: "123456",
            Product: new OffProduct(
                ProductName: "Test Product",
                Nutrition: new OffNutrition(
                    AggregatedSet: new OffAggregatedNutritionSet(
                        Nutrients: new Dictionary<string, OffNutrient>()
                    )
                )
            )
        );

        var result = response.ToFoodCacheResponse();

        Assert.AreEqual(0, result.Calories);
        Assert.AreEqual(0, result.Protein);
        Assert.AreEqual(0, result.Fat);
        Assert.AreEqual(0, result.Carbohydrates);
    }

    [TestMethod]
    public void ToFoodCacheResponse_WhenProductNameMissing_UsesUnknown()
    {
        var response = new OffProductResponse(
            Code: "123456",
            Product: new OffProduct(
                ProductName: null,
                Nutrition: new OffNutrition(
                    AggregatedSet: new OffAggregatedNutritionSet(
                        Nutrients: new Dictionary<string, OffNutrient>()
                    )
                )
            )
        );

        var result = response.ToFoodCacheResponse();

        Assert.AreEqual("Unknown", result.Name);
    }

    [TestMethod]
    public void ToFoodCacheResponse_WhenProductMissing_UsesFallbacks()
    {
        var response = new OffProductResponse(
            Code: "123456",
            Product: null
        );

        var result = response.ToFoodCacheResponse();

        Assert.AreEqual("Unknown", result.Name);
        Assert.AreEqual("123456", result.Barcode);
        Assert.AreEqual(0, result.Calories);
        Assert.AreEqual(0, result.Protein);
        Assert.AreEqual(0, result.Fat);
        Assert.AreEqual(0, result.Carbohydrates);
    }
}
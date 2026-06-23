using CalorieTracker.Api.Dtos.FoodCache;
using CalorieTracker.Api.Validators.FoodCache;

namespace CalorieTracker.Api.Tests.Validators.FoodCache;

[TestClass]
public class FoodCacheRequestValidatorTests
{
    [TestMethod]
    public void Validate_WhenBarcodeProvided_Passes()
    {
        var validator = new FoodCacheRequestValidator();

        var result = validator.Validate(new FoodCacheRequest("123456"));

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    public void Validate_WhenBarcodeMissing_Fails(string barcode)
    {
        var validator = new FoodCacheRequestValidator();

        var result = validator.Validate(new FoodCacheRequest(barcode));

        Assert.IsFalse(result.IsValid);
    }
}
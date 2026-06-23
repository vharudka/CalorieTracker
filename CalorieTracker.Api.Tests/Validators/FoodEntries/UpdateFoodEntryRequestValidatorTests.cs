using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Validators.FoodEntries;

namespace CalorieTracker.Api.Tests.Validators.FoodEntries;

[TestClass]
public class UpdateFoodEntryRequestValidatorTests
{
    [TestMethod]
    public void Validate_WhenAllFieldsValid_Passes()
    {
        var validator = new UpdateFoodEntryRequestValidator();

        var request = new UpdateFoodEntryRequest(
            Barcode: "123456",
            Grams: 100m,
            EatenAt: DateTime.UtcNow
        );

        var result = validator.Validate(request);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    public void Validate_WhenBarcodeMissing_Fails(string barcode)
    {
        var validator = new UpdateFoodEntryRequestValidator();

        var request = new UpdateFoodEntryRequest(
            Barcode: barcode,
            Grams: 100m,
            EatenAt: DateTime.UtcNow
        );

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("0")]
    [DataRow("-1")]
    [DataRow("-50.5")]
    public void Validate_WhenGramsNotPositive_Fails(string gramsValue)
    {
        var grams = decimal.Parse(gramsValue);

        var validator = new UpdateFoodEntryRequestValidator();

        var request = new UpdateFoodEntryRequest(
            Barcode: "123456",
            Grams: grams,
            EatenAt: DateTime.UtcNow
        );

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenEatenAtMissing_Fails()
    {
        var validator = new UpdateFoodEntryRequestValidator();

        var request = new UpdateFoodEntryRequest(
            Barcode: "123456",
            Grams: 100m,
            EatenAt: default
        );

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }
}
using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Validators.Stats;

namespace CalorieTracker.Api.Tests.Validators.Stats;

[TestClass]
public class MonthlyStatsRequestValidatorTests
{
    [TestMethod]
    public void Validate_WhenYearAndMonthValid_Passes()
    {
        var validator = new MonthlyStatsRequestValidator();

        var request = new MonthlyStatsRequest(
            Year: 2026,
            Month: 5
        );

        var result = validator.Validate(request);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Validate_WhenYearNotPositive_Fails(int year)
    {
        var validator = new MonthlyStatsRequestValidator();

        var request = new MonthlyStatsRequest(
            Year: year,
            Month: 5
        );

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(13)]
    [DataRow(-1)]
    public void Validate_WhenMonthOutOfRange_Fails(int month)
    {
        var validator = new MonthlyStatsRequestValidator();

        var request = new MonthlyStatsRequest(
            Year: 2026,
            Month: month
        );

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }
}
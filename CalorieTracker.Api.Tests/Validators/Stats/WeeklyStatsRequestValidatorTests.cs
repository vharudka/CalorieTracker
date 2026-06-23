using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Validators.Stats;

namespace CalorieTracker.Api.Tests.Validators.Stats;

[TestClass]
public class WeeklyStatsRequestValidatorTests
{
    [TestMethod]
    public void Validate_WhenDateProvided_Passes()
    {
        var validator = new WeeklyStatsRequestValidator();

        var request = new WeeklyStatsRequest(DateTime.UtcNow);

        var result = validator.Validate(request);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenDateMissing_Fails()
    {
        var validator = new WeeklyStatsRequestValidator();

        var request = new WeeklyStatsRequest(default);

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }
}
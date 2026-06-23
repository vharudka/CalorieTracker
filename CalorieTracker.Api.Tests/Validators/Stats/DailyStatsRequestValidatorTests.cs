using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Validators.Stats;

namespace CalorieTracker.Api.Tests.Validators.Stats;

[TestClass]
public class DailyStatsRequestValidatorTests
{
    [TestMethod]
    public void Validate_WhenDateProvided_Passes()
    {
        var validator = new DailyStatsRequestValidator();

        var request = new DailyStatsRequest(DateTime.UtcNow);

        var result = validator.Validate(request);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenDateMissing_Fails()
    {
        var validator = new DailyStatsRequestValidator();

        var request = new DailyStatsRequest(default);

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }
}
using CalorieTracker.Api.Dtos.UserGoals;
using CalorieTracker.Api.Validators.UserGoals;

namespace CalorieTracker.Api.Tests.Validators.UserGoals;

[TestClass]
public class SetUserGoalsRequestValidatorTests
{
    [TestMethod]
    public void Validate_WhenDailyCalorieLimitValid_Passes()
    {
        var validator = new SetUserGoalsRequestValidator();

        var request = new SetUserGoalsRequest(2000);

        var result = validator.Validate(request);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(-500)]
    public void Validate_WhenDailyCalorieLimitNotPositive_Fails(int limit)
    {
        var validator = new SetUserGoalsRequestValidator();

        var request = new SetUserGoalsRequest(limit);

        var result = validator.Validate(request);

        Assert.IsFalse(result.IsValid);
    }
}
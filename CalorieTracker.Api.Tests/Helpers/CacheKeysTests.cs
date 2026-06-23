using CalorieTracker.Api.Helpers;

namespace CalorieTracker.Api.Tests.Helpers;

[TestClass]
public class CacheKeysTests
{
    [TestMethod]
    public void UserGoals_WhenCalled_ReturnsCorrectKey()
    {
        var userId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        var result = CacheKeys.UserGoals(userId);

        Assert.AreEqual("usergoals:aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", result);
    }

    [TestMethod]
    public void FoodCacheKey_WhenCalled_ReturnsCorrectKey()
    {
        var barcode = "123456";

        var result = CacheKeys.FoodCacheKey(barcode);

        Assert.AreEqual("food:cache:123456", result);
    }
}
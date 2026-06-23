using CalorieTracker.Api.Helpers;

namespace CalorieTracker.Api.Tests.Helpers;

[TestClass]
public class PasswordHelperTests
{
    [TestMethod]
    public void GenerateSalt_WhenCalled_ReturnsNonEmptyString()
    {
        var salt = PasswordHelper.GenerateSalt();

        Assert.IsFalse(string.IsNullOrWhiteSpace(salt));
    }

    [TestMethod]
    public void HashPassword_WhenCalled_ReturnsConsistentHash()
    {
        var password = "mypassword";
        var salt = "fixedsalt";

        var hash1 = PasswordHelper.HashPassword(password, salt);
        var hash2 = PasswordHelper.HashPassword(password, salt);

        Assert.AreEqual(hash1, hash2);
    }

    [TestMethod]
    public void Verify_WhenPasswordMatches_ReturnsTrue()
    {
        var password = "mypassword";
        var salt = "fixedsalt";
        var hash = PasswordHelper.HashPassword(password, salt);

        var result = PasswordHelper.Verify(password, salt, hash);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Verify_WhenPasswordDoesNotMatch_ReturnsFalse()
    {
        var salt = "fixedsalt";
        var hash = PasswordHelper.HashPassword("correct", salt);

        var result = PasswordHelper.Verify("wrong", salt, hash);

        Assert.IsFalse(result);
    }
}
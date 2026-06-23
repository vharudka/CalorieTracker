using System.Security.Claims;
using CalorieTracker.Api.Extensions;

namespace CalorieTracker.Api.Tests.Extensions;

[TestClass]
public class UserExtensionsTests
{
    [TestMethod]
    public void GetUserId_WhenIdClaimPresent_ReturnsGuid()
    {
        var userId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        var claims = new[]
        {
            new Claim("id", userId.ToString())
        };

        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(claims)
        );

        var result = principal.GetUserId();

        Assert.AreEqual(userId, result);
    }
}
using CalorieTracker.Api.Helpers;
using CalorieTracker.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CalorieTracker.Api.Tests.Helpers;

[TestClass]
public class JwtHelperTests
{
    [TestMethod]
    public void GenerateToken_WhenCalled_ReturnsValidJwt()
    {
        var user = new User
        (
            Id: Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Username: "testuser",
            PasswordHash: "hash",
            PasswordSalt: "salt"
        );

        var settings = new Dictionary<string, string?>
        {
            { "Jwt:Key", "supersecretkeysupersecretkey1234" },
            { "Jwt:Issuer", "test-issuer" },
            { "Jwt:Audience", "test-audience" },
            { "Jwt:ExpiresMinutes", "30" }
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        var tokenString = JwtHelper.GenerateToken(user, config);

        var handler = new JwtSecurityTokenHandler();
        Assert.IsTrue(handler.CanReadToken(tokenString));

        var token = handler.ReadJwtToken(tokenString);

        Assert.AreEqual("test-issuer", token.Issuer);
        Assert.AreEqual("test-audience", token.Audiences.Single());

        var idClaim = token.Claims.FirstOrDefault(c => c.Type == "id");
        var usernameClaim = token.Claims.FirstOrDefault(c => c.Type == "username");

        Assert.IsNotNull(idClaim);
        Assert.IsNotNull(usernameClaim);

        Assert.AreEqual("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", idClaim!.Value);
        Assert.AreEqual("testuser", usernameClaim!.Value);

        Assert.AreEqual(SecurityAlgorithms.HmacSha256, token.Header.Alg);
    }
}
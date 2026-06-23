using CalorieTracker.Api.Dtos.Auths;
using CalorieTracker.Api.Options;
using CalorieTracker.Api.Validators.Auths;
using Microsoft.Extensions.Options;

namespace CalorieTracker.Api.Tests.Validators.Auths;

[TestClass]
public class RegisterRequestValidatorTests
{
    [TestMethod]
    [DataRow("user", "abcdef")]
    [DataRow("john", "123456")]
    [DataRow("vlad", "password")]
    public void Validate_WhenInputIsValid_Passes(string username, string password)
    {
        var options = new OptionsWrapper<PasswordValidationOptions>(
            new PasswordValidationOptions { MinimumLength = 6 });

        var validator = new RegisterRequestValidator(options);

        var result = validator.Validate(new RegisterRequest(username, password));

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    public void Validate_WhenUsernameIsMissing_Fails(string username)
    {
        var options = new OptionsWrapper<PasswordValidationOptions>(
            new PasswordValidationOptions { MinimumLength = 6 });

        var validator = new RegisterRequestValidator(options);

        var result = validator.Validate(new RegisterRequest(username, "abcdef"));

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("a")]
    [DataRow("ab")]
    public void Validate_WhenUsernameTooShort_Fails(string username)
    {
        var options = new OptionsWrapper<PasswordValidationOptions>(
            new PasswordValidationOptions { MinimumLength = 6 });

        var validator = new RegisterRequestValidator(options);

        var result = validator.Validate(new RegisterRequest(username, "abcdef"));

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    [DataRow("a")]
    [DataRow("abc")]
    [DataRow("12345")]
    public void Validate_WhenPasswordTooShort_Fails(string password)
    {
        var options = new OptionsWrapper<PasswordValidationOptions>(
            new PasswordValidationOptions { MinimumLength = 6 });

        var validator = new RegisterRequestValidator(options);

        var result = validator.Validate(new RegisterRequest("user", password));

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenUppercaseRequiredAndMissing_Fails()
    {
        var options = new OptionsWrapper<PasswordValidationOptions>(
            new PasswordValidationOptions
            {
                MinimumLength = 6,
                RequireUppercase = true
            });

        var validator = new RegisterRequestValidator(options);

        var result = validator.Validate(new RegisterRequest("user", "abcdef"));

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenDigitRequiredAndMissing_Fails()
    {
        var options = new OptionsWrapper<PasswordValidationOptions>(
            new PasswordValidationOptions
            {
                MinimumLength = 6,
                RequireDigit = true
            });

        var validator = new RegisterRequestValidator(options);

        var result = validator.Validate(new RegisterRequest("user", "abcdef"));

        Assert.IsFalse(result.IsValid);
    }

    [TestMethod]
    public void Validate_WhenSpecialRequiredAndMissing_Fails()
    {
        var options = new OptionsWrapper<PasswordValidationOptions>(
            new PasswordValidationOptions
            {
                MinimumLength = 6,
                RequireSpecial = true
            });

        var validator = new RegisterRequestValidator(options);

        var result = validator.Validate(new RegisterRequest("user", "Abcdef1"));

        Assert.IsFalse(result.IsValid);
    }
}
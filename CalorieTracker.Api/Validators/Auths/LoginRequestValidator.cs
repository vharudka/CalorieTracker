using CalorieTracker.Api.Dtos.Auths;
using CalorieTracker.Api.Options;
using Microsoft.Extensions.Options;

namespace CalorieTracker.Api.Validators.Auths;

public class LoginRequestValidator : AuthValidator<LoginRequest>
{
    public LoginRequestValidator(IOptions<PasswordValidationOptions> options)
        : base(options)
    {
    }
}
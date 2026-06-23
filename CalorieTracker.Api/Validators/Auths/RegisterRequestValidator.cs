using CalorieTracker.Api.Dtos.Auths;
using CalorieTracker.Api.Options;
using Microsoft.Extensions.Options;

namespace CalorieTracker.Api.Validators.Auths;

public class RegisterRequestValidator : AuthValidator<RegisterRequest>
{
    public RegisterRequestValidator(IOptions<PasswordValidationOptions> options)
        : base(options)
    {
    }
}
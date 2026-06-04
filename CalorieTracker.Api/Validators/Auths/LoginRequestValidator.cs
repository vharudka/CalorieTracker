using CalorieTracker.Api.Dtos.Auths;
using FluentValidation;

namespace CalorieTracker.Api.Validators.Auths;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(3)
            .WithMessage("Password must be at least 6 characters long.");
    }
}
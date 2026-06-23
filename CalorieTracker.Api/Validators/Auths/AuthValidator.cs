using CalorieTracker.Api.Dtos.Auths;
using CalorieTracker.Api.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace CalorieTracker.Api.Validators.Auths;

public class AuthValidator<T> : AbstractValidator<T>
    where T : AuthRequest
{
    protected AuthValidator(IOptions<PasswordValidationOptions> options)
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long.");

        var cfg = options.Value;

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(cfg.MinimumLength)
            .WithMessage($"Password must be at least {cfg.MinimumLength} characters long.");

        if (cfg.RequireUppercase)
        {
            RuleFor(x => x.Password)
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.");
        }

        if (cfg.RequireLowercase)
        {
            RuleFor(x => x.Password)
                .Matches("[a-z]")
                .WithMessage("Password must contain at least one lowercase letter.");
        }

        if (cfg.RequireDigit)
        {
            RuleFor(x => x.Password)
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one digit.");
        }

        if (cfg.RequireSpecial)
        {
            RuleFor(x => x.Password)
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least one special character.");
        }
    }
}
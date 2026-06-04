using CalorieTracker.Api.Dtos.Stats;
using FluentValidation;

namespace CalorieTracker.Api.Validators.Stats;

public class WeeklyStatsRequestValidator : AbstractValidator<WeeklyStatsRequest>
{
    public WeeklyStatsRequestValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required.")
            .Must(x => x != default)
            .WithMessage("Date must be a valid date");
    }
}
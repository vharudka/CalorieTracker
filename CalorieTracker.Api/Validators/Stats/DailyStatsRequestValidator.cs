using CalorieTracker.Api.Dtos.Stats;
using FluentValidation;

namespace CalorieTracker.Api.Validators.Stats;

public class DailyStatsRequestValidator : AbstractValidator<DailyStatsRequest>
{
    public DailyStatsRequestValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required.")
            .Must(x => x != default)
            .WithMessage("Date must be a valid date");
    }
}
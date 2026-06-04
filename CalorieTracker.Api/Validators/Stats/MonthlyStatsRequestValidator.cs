using CalorieTracker.Api.Dtos.Stats;
using FluentValidation;

namespace CalorieTracker.Api.Validators.Stats;

public class MonthlyStatsRequestValidator : AbstractValidator<MonthlyStatsRequest>
{
    public MonthlyStatsRequestValidator()
    {
        RuleFor(x => x.Month)
            .GreaterThan(0)
            .WithMessage("Month is required.");

        RuleFor(x => x.Month)
            .GreaterThan(0)
            .WithMessage("Month is required.");
    }
}
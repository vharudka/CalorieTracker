using CalorieTracker.Api.Dtos.Stats;
using FluentValidation;

namespace CalorieTracker.Api.Validators.Stats;

public class MonthlyStatsRequestValidator : AbstractValidator<MonthlyStatsRequest>
{
    public MonthlyStatsRequestValidator()
    {
        RuleFor(x => x.Year)
            .GreaterThan(0)
            .WithMessage("Year is required.");

        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12)
            .WithMessage("Month must be between 1 and 12.");
    }
}
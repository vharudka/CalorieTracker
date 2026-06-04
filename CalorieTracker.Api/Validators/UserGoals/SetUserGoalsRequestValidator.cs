using CalorieTracker.Api.Dtos.UserGoals;
using FluentValidation;

namespace CalorieTracker.Api.Validators.UserGoals;

public class SetUserGoalsRequestValidator : AbstractValidator<SetUserGoalsRequest>
{
    public SetUserGoalsRequestValidator()
    {
        RuleFor(x => x.DailyCalorieLimit)
            .GreaterThan(0)
            .WithMessage("Daily calorie limit must be greater than 0");
    }
}
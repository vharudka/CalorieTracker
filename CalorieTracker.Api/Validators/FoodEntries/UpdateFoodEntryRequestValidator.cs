using CalorieTracker.Api.Dtos.FoodEntries;
using FluentValidation;

namespace CalorieTracker.Api.Validators.FoodEntries;

public class UpdateFoodEntryRequestValidator : AbstractValidator<UpdateFoodEntryRequest>
{
    public UpdateFoodEntryRequestValidator()
    {
        RuleFor(x => x.Barcode)
            .NotEmpty()
            .WithMessage("Barcode is required.");

        RuleFor(x => x.Grams)
            .GreaterThan(0m)
            .WithMessage("Grams must be greater than 0.");

        RuleFor(x => x.EatenAt)
            .NotEmpty()
            .WithMessage("EatenAt is required.")
            .Must(x => x != default)
            .WithMessage("EatenAt must be a valid date");
    }
}
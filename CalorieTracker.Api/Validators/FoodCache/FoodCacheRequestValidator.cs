using CalorieTracker.Api.Dtos.FoodCache;
using FluentValidation;

namespace CalorieTracker.Api.Validators.FoodCache;

public class FoodCacheRequestValidator : AbstractValidator<FoodCacheRequest>
{
    public FoodCacheRequestValidator()
    {
        RuleFor(x => x.Barcode)
            .NotEmpty()
            .WithMessage("Barcode is required.");
    }
}
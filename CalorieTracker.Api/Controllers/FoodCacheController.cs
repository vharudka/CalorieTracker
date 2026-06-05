using CalorieTracker.Api.Dtos.FoodCache;
using CalorieTracker.Api.Services.FoodCache;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("api/food-cache")]
[Authorize]
public class FoodCacheController : ControllerBase
{
    private readonly IFoodCacheService _service;
    private readonly IValidator<FoodCacheRequest> _foodCacheValidator;
    private readonly ILogger<FoodCacheController> _logger;

    public FoodCacheController
    (
        IFoodCacheService service,
        IValidator<FoodCacheRequest> foodCacheValidator,
        ILogger<FoodCacheController> logger
    )
    {
        _service = service;
        _foodCacheValidator = foodCacheValidator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<FoodCacheResponse?>> GetAsync([FromQuery] FoodCacheRequest request)
    {
        _logger.LogInformation("Get food cache by barcode request received for Barcode {Barcode}", request.Barcode);

        var validation = await _foodCacheValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Get food cache validation failed for barcode {Barcode}: {@Errors}", request.Barcode, validation.Errors);
            return BadRequest(validation.Errors);
        }

        var result = await _service.GetAsync(request.Barcode);

        _logger.LogInformation("Food cache with Barcode {Barcode} retrieved successfully", request.Barcode);

        return Ok(result);
    }
}
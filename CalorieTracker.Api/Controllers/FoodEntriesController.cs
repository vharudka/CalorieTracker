using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Extensions;
using CalorieTracker.Api.Services.FoodEntries;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("api/food-entries")]
[Authorize]
public class FoodEntriesController : ControllerBase
{
    private readonly IFoodEntriesService _service;
    private readonly IValidator<CreateFoodEntryRequest> _createFoodEntryValidator;
    private readonly IValidator<UpdateFoodEntryRequest> _updateFoodEntryValidator;
    private readonly ILogger<AuthController> _logger;

    public FoodEntriesController
    (
        IFoodEntriesService service,
        IValidator<CreateFoodEntryRequest> createFoodEntryValidator,
        IValidator<UpdateFoodEntryRequest> updateFoodEntryValidator,
        ILogger<AuthController> logger
    )
    {
        _service = service;
        _createFoodEntryValidator = createFoodEntryValidator;
        _updateFoodEntryValidator = updateFoodEntryValidator;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<FoodEntryResponse>> Create(CreateFoodEntryRequest request)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Create food entry request received for user {UserId}", userId);

        var validation = await _createFoodEntryValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Create food entry validation failed for user {UserId}. Errors: {Errors}", userId, validation.Errors);
            return BadRequest(validation.Errors);
        }

        var result = await _service.CreateAsync(request, userId);

        _logger.LogInformation("Food entry {Id} created successfully for user {UserId}", result.Id, userId);

        return Created($"/api/food-entries/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FoodEntryResponse>> Update(Guid id, UpdateFoodEntryRequest request)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Update food entry request received for user {UserId}", userId);

        var validation = await _updateFoodEntryValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Update food entry validation failed for user {UserId}. Errors: {Errors}", userId, validation.Errors);
            return BadRequest(validation.Errors);
        }

        var result = await _service.UpdateAsync(id, userId, request);

        _logger.LogInformation("Food entry {Id} updated successfully for user {UserId}", id, userId);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FoodEntryResponse>> Get(Guid id)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Get food entry request received for entry {Id} by user {UserId}", id, userId);

        var result = await _service.GetAsync(id, userId);

        _logger.LogInformation("Food entry {Id} retrieved successfully for user {UserId}", id, userId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FoodEntryResponse>>> GetAll()
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Get all food entries request received for user {UserId}", userId);
        
        var result = await _service.GetAllByUserAsync(userId);

        _logger.LogInformation("Get all food entries request completed for user {UserId}", userId);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Delete food entry request received for entry {Id} by user {UserId}", id, userId);

        await _service.DeleteAsync(id, userId);

        _logger.LogInformation("Food entry {Id} deleted successfully for user {UserId}", id, userId);

        return NoContent();
    }
}
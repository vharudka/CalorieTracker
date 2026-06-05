using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Dtos.UserGoals;
using CalorieTracker.Api.Extensions;
using CalorieTracker.Api.Services.UserGoals;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("api/user-goals")]
[Authorize]
public class UserGoalsController : ControllerBase
{
    private readonly IUserGoalsService _service;
    private readonly IValidator<SetUserGoalsRequest> _setUserGoalsValidator;
    private readonly ILogger<UserGoalsController> _logger;

    public UserGoalsController
    (
        IUserGoalsService service,
        IValidator<SetUserGoalsRequest> setUserGoalsValidator,
        ILogger<UserGoalsController> logger
    )
    {
        _service = service;
        _setUserGoalsValidator = setUserGoalsValidator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<UserGoalsResponse>> Get()
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Get user goals request received for user {UserId}", userId);

        var goals = await _service.GetAsync(userId);
        
        _logger.LogInformation("User goals retrieved successfully for user {UserId}", userId);

        return Ok(goals);
    }

    [HttpPost]
    public async Task<ActionResult<UserGoalsResponse>> Upsert(SetUserGoalsRequest request)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Set user goals request received for user {UserId}", userId);

        var validation = await _setUserGoalsValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Set user goals validation failed for user {UserId}. Errors: {Errors}", userId, validation.Errors);
            return BadRequest(validation.Errors);
        }

        var result = await _service.UpsertAsync(userId, request);

        _logger.LogInformation("User goals set successfully for user {UserId}", userId);

        return Ok(result);
    }
}
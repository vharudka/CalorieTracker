using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Extensions;
using CalorieTracker.Api.Services.Stats;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("api/stats")]
[Authorize]
public class StatsController : ControllerBase
{
    private readonly IStatsService _service;
    private readonly IValidator<DailyStatsRequest> _dailyStatsValidator;
    private readonly IValidator<WeeklyStatsRequest> _weeklyStatsValidator;
    private readonly IValidator<MonthlyStatsRequest> _monthlyStatsValidator;
    private readonly ILogger<StatsController> _logger;

    public StatsController
    (
        IStatsService service,
        IValidator<DailyStatsRequest> dailyStatsValidator,
        IValidator<WeeklyStatsRequest> weeklyStatsValidator,
        IValidator<MonthlyStatsRequest> monthlyStatsValidator,
        ILogger<StatsController> logger
    )
    {
        _service = service;
        _dailyStatsValidator = dailyStatsValidator;
        _weeklyStatsValidator = weeklyStatsValidator;
        _monthlyStatsValidator = monthlyStatsValidator;
        _logger = logger;
    }

    [HttpGet("daily")]
    public async Task<ActionResult<DailyStatsResponse>> GetDaily([FromQuery] DailyStatsRequest request)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Get daily stats for user {UserId} for date {Date}", userId, request.Date);

        var validation = await _dailyStatsValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Get daily stats validation failed for user {UserId} for date {Date}. Errors: {Errors}", userId, request.Date, validation.Errors);
            return BadRequest(validation.Errors);
        }

        var result = await _service.GetDailyAsync(userId, request);

        _logger.LogInformation("Daily stats retrieved successfully for user {UserId} for date {Date}", userId, request.Date);

        return Ok(result);
    }

    [HttpGet("weekly")]
    public async Task<ActionResult<WeeklyStatsResponse>> GetWeekly([FromQuery] WeeklyStatsRequest request)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Get weekly stats for user {UserId} for date {Date}", userId, request.Date);

        var validation = await _weeklyStatsValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Get weekly stats validation failed for user {UserId} for date {Date}. Errors: {Errors}", userId, request.Date, validation.Errors);
            return BadRequest(validation.Errors);
        }

        var result = await _service.GetWeeklyAsync(userId, request);

        _logger.LogInformation("Weekly stats retrieved successfully for user {UserId} for date {Date}", userId, request.Date);

        return Ok(result);
    }

    [HttpGet("monthly")]
    public async Task<ActionResult<MonthlyStatsResponse>> GetMonthly([FromQuery] MonthlyStatsRequest request)
    {
        var userId = User.GetUserId();

        _logger.LogInformation("Get monthly stats for user {UserId} for year {Year} and month {Month}", userId, request.Year, request.Month);

        var validation = await _monthlyStatsValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Get monthly stats validation failed for user {UserId} for year {Year} and month {Month}. Errors: {Errors}", userId, request.Year, request.Month, validation.Errors);
            return BadRequest(validation.Errors);
        }

        var result = await _service.GetMonthlyAsync(userId, request);

        _logger.LogInformation("Monthly stats retrieved successfully for user {UserId} for year {Year} and month {Month}", userId, request.Year, request.Month);

        return Ok(result);
    }
}
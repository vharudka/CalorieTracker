using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Extensions;
using CalorieTracker.Api.Services.Stats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("stats")]
[Authorize]
public class StatsController : ControllerBase
{
    private readonly IStatsService _service;

    public StatsController(IStatsService service)
    {
        _service = service;
    }

    [HttpGet("daily")]
    public async Task<ActionResult<DailyStatsResponse>> GetDaily([FromQuery] DailyStatsRequest request)
    {
        var userId = User.GetUserId();

        var result = await _service.GetDailyAsync(userId, request);

        return Ok(result);
    }

    [HttpGet("weekly")]
    public async Task<ActionResult<WeeklyStatsResponse>> GetWeekly([FromQuery] WeeklyStatsRequest request)
    {
        var userId = User.GetUserId();

        var result = await _service.GetWeeklyAsync(userId, request);

        return Ok(result);
    }

    [HttpGet("monthly")]
    public async Task<ActionResult<MonthlyStatsResponse>> GetMonthly([FromQuery] MonthlyStatsRequest request)
    {
        var userId = User.GetUserId();

        var result = await _service.GetMonthlyAsync(userId, request);

        return Ok(result);
    }
}
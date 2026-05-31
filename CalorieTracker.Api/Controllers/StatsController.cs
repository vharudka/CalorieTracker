using CalorieTracker.Api.Dtos.Stats;
using CalorieTracker.Api.Extensions;
using CalorieTracker.Api.Services;
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
    public async Task<ActionResult<DailyStatsResponse>> GetDaily([FromQuery] DateTime date)
    {
        var userId = User.GetUserId();

        var result = await _service.GetDailyAsync(userId, date);

        return Ok(result);
    }

    [HttpGet("weekly")]
    public async Task<ActionResult<WeeklyStatsResponse>> GetWeekly([FromQuery] DateTime date)
    {
        var userId = User.GetUserId();

        var result = await _service.GetWeeklyAsync(userId, date);

        return result;
    }

    [HttpGet("monthly")]
    public async Task<ActionResult<MonthlyStatsResponse>> GetMonthly([FromQuery] int year, [FromQuery] int month)
    {
        var userId = User.GetUserId();

        var result = await _service.GetMonthlyAsync(userId, year, month);

        return Ok();
    }
}
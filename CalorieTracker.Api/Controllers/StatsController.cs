using CalorieTracker.Api.Dtos.Stats;
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
    public async Task<ActionResult<DailyStatsResponse>> GetDailyAsync([FromQuery] DateTime date)
    {
        var result = await _service.GetDailyAsync(date);
        return Ok(result);
    }

    [HttpGet("weekly")]
    public async Task<ActionResult<WeeklyStatsResponse>> GetWeeklyAsync([FromQuery] DateTime weekStart)
    {
        var result = await _service.GetWeeklyAsync(weekStart);
        return Ok(result);
    }

    [HttpGet("monthly")]
    public async Task<ActionResult<MonthlyStatsResponse>> GetMonthlyAsync([FromQuery] MonthlyStatsRequest request)
    {
        var result = await _service.GetMonthlyAsync(request);
        return Ok(result);
    }
}
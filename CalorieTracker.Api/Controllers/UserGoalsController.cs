using CalorieTracker.Api.Dtos.UserGoals;
using CalorieTracker.Api.Extensions;
using CalorieTracker.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("user-goals")]
[Authorize]
public class UserGoalsController : ControllerBase
{
    private readonly IUserGoalsService _service;

    public UserGoalsController(IUserGoalsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<UserGoalsResponse>> Get()
    {
        var userId = User.GetUserId();

        var goals = await _service.GetAsync(userId);

        return Ok(goals);
    }

    [HttpPost]
    public async Task<ActionResult<UserGoalsResponse>> Upsert(SetUserGoalsRequest request)
    {
        var userId = User.GetUserId();

        var result = await _service.UpsertAsync(userId, request);

        return Ok(result);
    }
}
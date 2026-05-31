using CalorieTracker.Api.Dtos.UserGoals;
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
    public async Task<ActionResult<UserGoalsResponse>> GetAsync()
    {
        var result = await _service.GetAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<UserGoalsResponse>> SetAsync(SetUserGoalsRequest request)
    {
        var result = await _service.SetAsync(request);
        return Ok(result);
    }
}
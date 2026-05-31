using CalorieTracker.Api.Dtos.FoodCache;
using CalorieTracker.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("api/food-cache")]
[Authorize]
public class FoodCacheController : ControllerBase
{
    private readonly IFoodCacheService _service;

    public FoodCacheController(IFoodCacheService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<SearchFoodResponse>> SearchAsync([FromQuery] string query)
    {
        var result = await _service.SearchAsync(query);
        return Ok(result);
    }
}
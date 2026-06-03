using CalorieTracker.Api.Dtos.FoodCache;
using CalorieTracker.Api.Services.FoodCache;
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
    public async Task<ActionResult<FoodCacheResponse?>> GetAsync([FromQuery] FoodCacheRequest request)
    {
        var result = await _service.GetAsync(request.Barcode);

        return Ok(result);
    }
}
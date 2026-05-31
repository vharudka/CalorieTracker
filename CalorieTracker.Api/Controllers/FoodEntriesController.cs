using CalorieTracker.Api.Dtos.FoodEntries;
using CalorieTracker.Api.Extensions;
using CalorieTracker.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[ApiController]
[Route("food-entries")]
[Authorize]
public class FoodEntriesController : ControllerBase
{
    private readonly IFoodEntriesService _service;

    public FoodEntriesController(IFoodEntriesService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<FoodEntryResponse>> Create(CreateFoodEntryRequest request)
    {
        var userId = User.GetUserId();
        var result = await _service.CreateAsync(request, userId);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FoodEntryResponse>> Update(Guid id, UpdateFoodEntryRequest request)
    {
        var userId = User.GetUserId();
        var result = await _service.UpdateAsync(id, userId, request);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FoodEntryResponse>> Get(Guid id)
    {
        var userId = User.GetUserId();
        var result = await _service.GetAsync(id, userId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodEntryResponse>>> GetAll()
    {
        var userId = User.GetUserId();
        var result = await _service.GetAllByUserAsync(userId);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.GetUserId();
        await _service.DeleteAsync(id, userId);

        return NoContent();
    }
}
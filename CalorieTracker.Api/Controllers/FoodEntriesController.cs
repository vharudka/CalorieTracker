using CalorieTracker.Api.Dtos.FoodEntries;
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
    public async Task<ActionResult<FoodEntryResponse>> CreateAsync(CreateFoodEntryRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FoodEntryResponse>> UpdateAsync(Guid id, UpdateFoodEntryRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using NotificationsService.Application.Services.Interfaces;
using ObjectiveManager.Models.NotificationsService.DTO;

namespace NotificationsService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FrequencyController : ControllerBase
{
    private readonly IFrequencyValuesService _frequencyValuesService;

    public FrequencyController(IFrequencyValuesService frequencyValuesService)
    {
        _frequencyValuesService = frequencyValuesService;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(FrequencyValueDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var statuses = await _frequencyValuesService.GetAll();
        return Ok(statuses);
    }
}
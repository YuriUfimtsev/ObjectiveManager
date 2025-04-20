using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Application.Services.Interfaces;

namespace ObjectivesService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusesController : ControllerBase
{
    private readonly IStatusValueService _statusValueService;

    public StatusesController(IStatusValueService statusValueService)
    {
        _statusValueService = statusValueService;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(StatusValueDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var statuses = await _statusValueService.GetAll();
        return Ok(statuses);
    }
}
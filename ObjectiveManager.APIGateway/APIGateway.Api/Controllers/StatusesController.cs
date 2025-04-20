using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Client;

namespace APIGateway.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StatusesController : ControllerBase
{
    private readonly IObjectivesServiceClient _objectivesServiceClient;

    public StatusesController(IObjectivesServiceClient objectivesServiceClient)
    {
        _objectivesServiceClient = objectivesServiceClient;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(StatusValueDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var objectives = await _objectivesServiceClient.GetAllStatuses();
        return Ok(objectives);
    }
}
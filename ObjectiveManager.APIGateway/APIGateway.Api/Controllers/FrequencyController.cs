using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationsService.Client;
using ObjectiveManager.Models.NotificationsService.DTO;

namespace APIGateway.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FrequencyController : ControllerBase
{
    private readonly INotificationsServiceClient _notificationsServiceClient;

    public FrequencyController(INotificationsServiceClient notificationsServiceClient)
    {
        _notificationsServiceClient = notificationsServiceClient;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(FrequencyValueDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var frequencyValues = await _notificationsServiceClient.GetAllFrequencyValues();
        return Ok(frequencyValues);
    }
}
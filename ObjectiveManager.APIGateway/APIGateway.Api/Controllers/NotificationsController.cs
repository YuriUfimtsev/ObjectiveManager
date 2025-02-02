using Microsoft.AspNetCore.Mvc;
using NotificationsService.Client;
using ObjectiveManager.Models.NotificationsService.DTO;

namespace APIGateway.Api.Controllers;

public class NotificationsController : ControllerBase
{
    private readonly INotificationsServiceClient _notificationsServiceClient;

    public NotificationsController(INotificationsServiceClient notificationsServiceClient)
    {
        _notificationsServiceClient = notificationsServiceClient;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(NotificationDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var notificationResult = await _notificationsServiceClient.GetNotification();
        return notificationResult.Succeeded
            ? Ok(notificationResult.Value)
            : NotFound(notificationResult.Errors);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromQuery] NotificationPostDto notificationPostDto)
    {
        var notificationId = await _notificationsServiceClient.CreateNotification(notificationPostDto);
        return Ok(notificationId);
    }

    [HttpPut("updateFrequency/{notificationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatus([FromRoute] string notificationId, [FromQuery] long frequencyId)
    {
        var updateFrequencyResult = await _notificationsServiceClient.UpdateNotificationFrequency(notificationId, frequencyId);
        return updateFrequencyResult.Succeeded
            ? Ok()
            : BadRequest(updateFrequencyResult.Errors);
    }
}
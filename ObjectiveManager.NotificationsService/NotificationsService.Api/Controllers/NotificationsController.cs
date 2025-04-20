using Microsoft.AspNetCore.Mvc;
using NotificationsService.Application.Services.Interfaces;
using ObjectiveManager.Models.NotificationsService.DTO;
using ObjectiveManager.Models.Result;
using ObjectiveManager.Utils.Http;

namespace NotificationsService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationsService _notificationsService;

    public NotificationsController(INotificationsService notificationsService)
    {
        _notificationsService = notificationsService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Result<NotificationDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var userId = Request.GetUserIdFromHeader();
        var notificationResult = await _notificationsService.GetForUser(userId);
        return Ok(notificationResult);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromQuery] string userId)
    {
        var notificationId = await _notificationsService.Create(userId);
        return Ok(notificationId);
    }

    [HttpPut("updateFrequency/{notificationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateNotificationFrequency([FromRoute] Guid notificationId,
        [FromQuery] long frequencyValueId)
    {
        var updateResult = await _notificationsService.UpdateFrequency(notificationId, frequencyValueId);
        return Ok(updateResult);
    }
}
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectivesService.Client;

namespace APIGateway.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ObjectivesController : ControllerBase
{
    private readonly IObjectivesServiceClient _objectivesServiceClient;

    public ObjectivesController(IObjectivesServiceClient objectivesServiceClient)
    {
        _objectivesServiceClient = objectivesServiceClient;
    }

    [HttpGet("{objectiveId}")]
    [ProducesResponseType(typeof(ObjectiveDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string objectiveId)
    {
        var objectiveResult = await _objectivesServiceClient.GetObjectiveInfo(objectiveId);
        return objectiveResult.Succeeded
            ? Ok(objectiveResult.Value)
            : NotFound(objectiveResult.Errors);
    }

    [HttpGet("statusHistory/{objectiveId}")]
    [ProducesResponseType(typeof(StatusObjectDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistory([FromRoute] string objectiveId)
    {
        var statusObjectsResult = await _objectivesServiceClient.GetStatusesHistory(objectiveId);
        return statusObjectsResult.Succeeded
            ? Ok(statusObjectsResult.Value)
            : NotFound(statusObjectsResult.Errors);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(ObjectiveDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var objectives = await _objectivesServiceClient.GetAllObjectives();
        return Ok(objectives);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromQuery] ObjectivePostDto objectivePostDto)
    {
        var objectiveId = await _objectivesServiceClient.CreateObjective(objectivePostDto);
        return Ok(objectiveId);
    }

    [HttpPut("updateInfo/{objectiveId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateInfo([FromRoute] string objectiveId,
        [FromQuery] ObjectivePutDto objectivePutDto)
    {
        var updateResult = await _objectivesServiceClient.UpdateObjectiveInfo(objectiveId, objectivePutDto);
        return updateResult.Succeeded
            ? Ok()
            : BadRequest(updateResult.Errors);
    }

    [HttpPut("updateStatusObject/{objectiveId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatus([FromRoute] string objectiveId,
        [FromQuery] StatusObjectPutDTO updateObjectDto)
    {
        var updateStatusResult = await _objectivesServiceClient.UpdateObjectiveStatus(objectiveId, updateObjectDto);
        return updateStatusResult.Succeeded
            ? Ok()
            : BadRequest(updateStatusResult.Errors);
    }

    [HttpDelete("{objectiveId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] string objectiveId)
    {
        var deletionResult = await _objectivesServiceClient.DeleteObjective(objectiveId);
        return deletionResult.Succeeded
            ? Ok()
            : NotFound(deletionResult.Errors);
    }
}
using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectiveManager.Models.Result;
using ObjectiveManager.Utils.Http;
using ObjectivesService.Application.Models.Dto;
using ObjectivesService.Application.Services.Interfaces;
using ObjectivesService.Domain.DTO;

namespace ObjectivesService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObjectivesController : ControllerBase
{
    private readonly IObjectiveService _objectiveService;
    private readonly IStatusObjectService _statusObjectService;

    public ObjectivesController(
        IObjectiveService objectiveService,
        IStatusObjectService statusObjectService)
    {
        _objectiveService = objectiveService;
        _statusObjectService = statusObjectService;
    }

    [HttpGet("{objectiveId}")]
    [ProducesResponseType(typeof(Result<ObjectiveDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] Guid objectiveId)
    {
        var objectiveResult = await _objectiveService.Get(objectiveId);
        return Ok(objectiveResult);
    }

    [HttpGet("statusHistory/{objectiveId}")]
    [ProducesResponseType(typeof(StatusObjectDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistory([FromRoute] Guid objectiveId)
    {
        var statusObjects = await _statusObjectService.GetHistory(objectiveId);
        return Ok(statusObjects);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(ObjectiveDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var objectives = await _objectiveService.GetAll();
        return Ok(objectives);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] ObjectivePostDto objectivePostDto)
    {
        var userId = Request.GetUserIdFromHeader();
        var createObjectiveDto = new CreateObjectiveDto(
            Definition: objectivePostDto.Definition,
            FinalDate: objectivePostDto.FinalDate,
            Comment: objectivePostDto.Comment,
            UserId: userId
        );
        var objectiveId = await _objectiveService.CreateWithStatus(createObjectiveDto);

        return Ok(objectiveId);
    }

    [HttpPut("updateInfo/{objectiveId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateInfo([FromRoute] Guid objectiveId,
        [FromBody] ObjectivePutDto objectivePutDto)
    {
        var updateObjectiveDto = new UpdateObjectiveDTO(
            ObjectiveId: objectiveId,
            Definition: objectivePutDto.Definition,
            FinalDate: objectivePutDto.FinalDate,
            Comment: objectivePutDto.Comment
        );

        var updateResult = await _objectiveService.Update(updateObjectiveDto);
        return Ok(updateResult);
    }

    [HttpPut("updateStatusObject/{objectiveId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid objectiveId,
        [FromBody] StatusObjectPutDTO updateObjectDto)
    {
        // Создаём объект статуса
        var createStatusObjectDTO = new CreateStatusObjectDTO(
            ObjectiveId: objectiveId,
            StatusValueId: updateObjectDto.StatusValueId,
            Comment: updateObjectDto.StatusComment);
        var statusObjectId = await _statusObjectService.CreateObject(createStatusObjectDTO);

        // Обновляем объект статуса у цели
        var updateObjectiveDto = new UpdateStatusObjectDTO(
            ObjectiveId: objectiveId,
            StatusObjectId: statusObjectId);

        var updateStatusResult = await _objectiveService.UpdateStatusObject(updateObjectiveDto);
        return Ok(updateStatusResult);
    }

    [HttpDelete("{objectiveId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid objectiveId)
    {
        var deletionResult = await _objectiveService.Delete(objectiveId);
        return Ok(deletionResult);
    }
}
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Application;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Api.Dto;
using ObjectiveManager.Domain.Dto;

namespace ObjectiveManager.Api.Controllers;

// todo: корректная обработка dateTime и маппинг статусов задач
[ApiController]
[Route("[controller]")]
public class ObjectivesController : ControllerBase
{
    private readonly IObjectiveService _objectiveService;
    private readonly IMapper _mapper;

    public ObjectivesController(
        IObjectiveService objectiveService,
        IMapper mapper)
    {
        _objectiveService = objectiveService;
        _mapper = mapper;
    }
    
    [HttpGet("{objectiveId}")]
    [ProducesResponseType(typeof(Objective), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get([FromRoute] string objectiveId)
    {
        var objective = _objectiveService.Get(objectiveId);
        return objective is not null ? Ok(objective) : NotFound();
    }
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(Objective[]), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var objectives = _objectiveService.GetAll();
        return Ok(objectives);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult Add([FromQuery] ObjectivePostDto objectivePostDto)
    {
        var createObjectiveDto = _mapper.Map<CreateObjectiveDto>(objectivePostDto);
        return Ok(_objectiveService.Create(createObjectiveDto));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Update([FromQuery] ObjectiveUpdateDto objectiveUpdateDto)
    {
        try
        {
            var updatedObjective = _mapper.Map<Objective>(objectiveUpdateDto);
            _objectiveService.Update(updatedObjective);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{objectiveId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] string objectiveId)
    {
        try
        {
            _objectiveService.Delete(objectiveId);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
}
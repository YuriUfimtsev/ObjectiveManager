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
public class ObjectiveController : ControllerBase
{
    private readonly IObjectiveService _objectiveService;
    private readonly IMapper _mapper;

    public ObjectiveController(
        IObjectiveService objectiveService,
        IMapper mapper)
    {
        _objectiveService = objectiveService;
        _mapper = mapper;
    }
    
    [HttpGet("{objectiveId}")]
    public IActionResult Get([FromRoute] string objectiveId)
    {
        var objective = _objectiveService.Get(objectiveId);
        return objective is not null ? Ok(objective) : NotFound();
    }

    [HttpPost]
    public IActionResult Add([FromQuery] ObjectivePostDto objectivePostDto)
    {
        var createObjectiveDto = _mapper.Map<CreateObjectiveDto>(objectivePostDto);
        return Ok(_objectiveService.Create(createObjectiveDto));
    }

    [HttpPut]
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
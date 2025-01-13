using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Application;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Api.Dto;
using ObjectiveManager.Application.Dto;
using ObjectiveManager.Application.Services;
using ObjectiveManager.Domain.Dto;

namespace ObjectiveManager.Api.Controllers;

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
    public async Task<IActionResult> Get([FromRoute] string objectiveId)
    {
        var objective = await _objectiveService.Get(objectiveId);
        return objective is not null ? Ok(objective) : NotFound();
    }
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(Objective[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var objectives = await _objectiveService.GetAll();
        return Ok(objectives);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add([FromQuery] ObjectivePostDto objectivePostDto)
    {
        var createObjectiveDto = _mapper.Map<CreateObjectiveDto>(objectivePostDto);
        var objectiveId = await _objectiveService.Create(createObjectiveDto);
        return Ok(objectiveId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromQuery] ObjectivePutDto objectivePutDto)
    {
        try
        {
            var updateObjectiveDto = _mapper.Map<UpdateObjectiveDto>(objectivePutDto);
            await _objectiveService.Update(updateObjectiveDto);
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
    public async Task<IActionResult> Delete([FromRoute] string objectiveId)
    {
        try
        {
            await _objectiveService.Delete(objectiveId);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
}
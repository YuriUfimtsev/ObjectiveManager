using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Application.Models;
using ObjectiveManager.Application.Services;

namespace ObjectiveManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusesController : ControllerBase
{
    private readonly IStatusesService _statusesService;
    private readonly IMapper _mapper;

    public StatusesController(IMapper mapper, IStatusesService statusesService)
    {
        _mapper = mapper;
        _statusesService = statusesService;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(ObjectiveStatus[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var statuses = await _statusesService.GetAll();
        return Ok(statuses);
    }

}
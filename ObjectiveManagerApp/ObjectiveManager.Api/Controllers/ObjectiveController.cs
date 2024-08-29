using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Application;
using ObjectiveManager.Domain.Models;

namespace ObjectiveManager.Api.Controllers;

// todo: сделать асинхронным
// todo: обработка ошибок (исключений)
// todo: корректная обработка dateTime и маппинг статусов задач
[ApiController]
[Route("[controller]")]
public class ObjectiveController(IObjectiveService objectiveService) : ControllerBase
{
    [HttpGet("{objectiveId}")]
    public IActionResult Get([FromRoute] string objectiveId)
      => Ok(objectiveService.Get(objectiveId));
    
    [HttpPost]
    public IActionResult Add([FromQuery] ObjectiveCreation newObjective)
        => Ok(objectiveService.Create(newObjective));

    [HttpPut]
    public IActionResult Update([FromQuery] Objective newObjective)
        => Ok(objectiveService.Update(newObjective));
    
    [HttpDelete("{objectiveId}")]
    public IActionResult Delete([FromRoute] string objectiveId)
        => Ok(objectiveService.Delete(objectiveId));
}
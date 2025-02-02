using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ObjectivesService.Application.Services.Interfaces;

namespace ObjectivesService.Api.Filters;

public class ObjectiveCreatorOnlyAttribute : ActionFilterAttribute
{
    private readonly IObjectiveService _objectiveService;

    public ObjectiveCreatorOnlyAttribute(IObjectiveService objectiveService)
    {
        _objectiveService = objectiveService;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var headers = context.HttpContext.Request.Headers;
        headers.TryGetValue("UserId", out var currentUserId);

        var routeData = context.HttpContext.GetRouteData();
        if (routeData.Values.TryGetValue("objectiveId", out var objectiveId))
        {
            if (!Guid.TryParse(objectiveId?.ToString(), out var objectiveIdGuid))
            {
                context.Result = CreateErrorResult("Ошибка сервера: тип Id цели не является Guid");
                return;
            }

            var objectiveCreatorIdResult = await _objectiveService.GetCreatorId(objectiveIdGuid);
            if (!objectiveCreatorIdResult.Succeeded)
            {
                context.Result = CreateErrorResult(
                    "Ошибка сервера: Id создателя цели не определено");
                return;
            }

            if (objectiveCreatorIdResult.Succeeded && currentUserId[0] != objectiveCreatorIdResult.Value)
            {
                context.Result = CreateErrorResult(
                    "Недостаточно прав: вы не являетесь создателем цели");
                return;
            }
        }

        await next();
    }

    private static ContentResult CreateErrorResult(string message)
        => new()
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Content = message,
            ContentType = MediaTypeNames.Application.Json
        };
}
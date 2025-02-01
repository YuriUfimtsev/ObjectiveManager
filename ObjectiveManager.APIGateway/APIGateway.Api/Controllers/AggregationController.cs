using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Api.Controllers;

public class AggregationController : ControllerBase
{
    protected string? UserId =>
        Request.HttpContext.User.Claims
            .FirstOrDefault(claim => claim.Type.ToString() == "_id")
            ?.Value;
}
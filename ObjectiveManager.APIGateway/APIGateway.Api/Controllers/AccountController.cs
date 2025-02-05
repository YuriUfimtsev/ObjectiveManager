using System.IdentityModel.Tokens.Jwt;
using System.Net;
using AuthService.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationsService.Client;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.AuthService.Dto;

namespace APIGateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthServiceClient _authServiceClient;
    private readonly INotificationsServiceClient _notificationsServiceClient;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AccountController(IAuthServiceClient authServiceClient,
        INotificationsServiceClient notificationsServiceClient, JwtSecurityTokenHandler tokenHandler)
    {
        _authServiceClient = authServiceClient;
        _notificationsServiceClient = notificationsServiceClient;
        _tokenHandler = tokenHandler;
    }

    [Authorize]
    [HttpGet("userData")]
    [ProducesResponseType(typeof(AccountData), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserData()
    {
        var userDataResult = await _authServiceClient.GetUserData();
        return userDataResult.Succeeded
            ? Ok(userDataResult.Value)
            : NotFound(userDataResult.Errors);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenCredentials), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        // Регистрируем пользователя
        var tokenCredentialsResult = await _authServiceClient.RegisterUser(model);
        if (!tokenCredentialsResult.Succeeded)
        {
            return BadRequest(tokenCredentialsResult.Errors);
        }

        // В случае успешной регистрации создаем объект для нотификаций.
        // Пока у пользователя будет 0 целей, нотификации не отправляются
        var userId = _tokenHandler.ReadJwtToken(tokenCredentialsResult.Value.AccessToken)
            .Claims
            .FirstOrDefault(claim => claim.Type == "_id")?
            .Value;
        await _notificationsServiceClient.CreateNotification(userId!);
        
        return Ok(tokenCredentialsResult.Value);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenCredentials), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        var tokenCredentialsResult = await _authServiceClient.LoginUser(model);
        return tokenCredentialsResult.Succeeded
            ? Ok(tokenCredentialsResult.Value)
            : NotFound(tokenCredentialsResult.Errors);
    }

    [Authorize]
    [HttpPut("edit")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Edit([FromBody] EditAccountDto model)
    {
        var editResult = await _authServiceClient.EditUser(model);
        return editResult.Succeeded
            ? Ok()
            : BadRequest(editResult.Errors);
    }
}
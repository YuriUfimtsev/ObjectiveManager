using System.Net;
using AuthService.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.AuthService.Dto;
using ObjectiveManager.Models.Result;

namespace APIGateway.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthServiceClient _authServiceClient;

    public AccountController(IAuthServiceClient authServiceClient)
    {
        _authServiceClient = authServiceClient;
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
        var tokenCredentialsResult = await _authServiceClient.RegisterUser(model);
        return tokenCredentialsResult.Succeeded
            ? Ok(tokenCredentialsResult.Value)
            : BadRequest(tokenCredentialsResult.Errors);
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
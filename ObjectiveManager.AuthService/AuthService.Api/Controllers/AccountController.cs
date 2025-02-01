using System.Net;
using AuthService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.AuthService.Dto;
using ObjectiveManager.Models.Result;
using ObjectiveManager.Utils.Http;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("userData")]
    [ProducesResponseType(typeof(Result<AccountData>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUserData()
    {
        var userId = Request.GetUserIdFromHeader();
        var accountDataResult = await _accountService.GetAccountDataAsync(userId);
        return Ok(accountDataResult);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<TokenCredentials>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
    {
        var registerResult = await _accountService.RegisterUserAsync(registerModel);
        return Ok(registerResult);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<TokenCredentials>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
    {
        var tokenCredentialsResult = await _accountService.LoginUserAsync(loginModel);
        return Ok(tokenCredentialsResult);
    }

    [HttpGet("refreshToken")]
    [ProducesResponseType(typeof(Result<TokenCredentials>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RefreshToken()
    {
        var userId = Request.GetUserIdFromHeader();
        var tokenCredentialsResult = await _accountService.RefreshToken(userId);
        return Ok(tokenCredentialsResult);
    }

    [HttpPut("edit")]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Edit([FromBody] EditAccountDto editUserInfoModel)
    {
        var userId = Request.GetUserIdFromHeader();
        var editResult = await _accountService.EditAccountAsync(userId, editUserInfoModel);
        return Ok(editResult);
    }
}
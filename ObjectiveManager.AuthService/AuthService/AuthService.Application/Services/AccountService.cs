using AuthService.Application.Services.Interfaces;
using AuthService.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.AuthService.Dto;
using ObjectiveManager.Models.Result;

namespace AuthService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUserManager _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthTokenService _authTokenService;
    private readonly IMapper _mapper;
    
    public AccountService(IUserManager userManager,
        SignInManager<User> signInManager, IAuthTokenService authTokenService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authTokenService = authTokenService;
        _mapper = mapper;
    }

    public async Task<Result<AccountData>> GetAccountDataAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result<AccountData>.Failed("Пользователь не найден");
        }

        var userData = new AccountData(
            UserId: user.Id,
            Name: user.Name,
            Surname: user.Surname,
            Email: user.Email!,
            MentorEmail: user.MentorEmail);
        return Result<AccountData>.Success(userData);
    }
    
    public async Task<Result<TokenCredentials>> RegisterUserAsync(RegisterDto model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return Result<TokenCredentials>.Failed("Пользователь с такой почтой уже зарегистрирован");
        }

        if (model.Password.Length < 6)
        {
            return Result<TokenCredentials>.Failed("Пароль должен содержать не менее 6 символов");
        }
        
        var user = _mapper.Map<User>(model);
        user.UserName = user.Email;

        var c = await _userManager.CreateAsync(user, model.Password);
        user.EmailConfirmed = true;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            return signInResult.Succeeded
                ? Result<TokenCredentials>.Success(_authTokenService.GetToken(user))
                : Result<TokenCredentials>.Failed(TryGetIdentityError(signInResult));
        }

        return Result<TokenCredentials>.Failed(result.Errors.Select(errors => errors.Description).ToArray());
    }

    public async Task<Result> EditAccountAsync(string id, EditAccountDto model)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return Result.Failed("Пользователь не найден");
        }

        var result = await ChangeUserDataTask(user, model);

        return result.Succeeded 
            ? Result.Success()
            : Result.Failed();
    }

    public async Task<Result<TokenCredentials>> LoginUserAsync(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Result<TokenCredentials>.Failed("Пользователь не найден");
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!signInResult.Succeeded)
        {
            return Result<TokenCredentials>.Failed(TryGetIdentityError(signInResult));
        }

        var tokenCredentials = _authTokenService.GetToken(user);
        return Result<TokenCredentials>.Success(tokenCredentials);
    }

    public async Task<Result<TokenCredentials>> RefreshToken(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result<TokenCredentials>.Failed("Пользователь не найден");
        }
        
        var token = _authTokenService.GetToken(user);
        return Result<TokenCredentials>.Success(token);
    }
    
    private Task<IdentityResult> ChangeUserDataTask(User user, EditAccountDto model)
    {
        if (!string.IsNullOrWhiteSpace(model.Email))
        {
            user.Email = model.Email;
        }

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            user.Name = model.Name;
        }

        if (!string.IsNullOrWhiteSpace(model.Surname))
        {
            user.Surname = model.Surname;
        }
        
        if (!string.IsNullOrWhiteSpace(model.MentorEmail))
        {
            user.MentorEmail = model.MentorEmail;
        }
        
        return _userManager.UpdateAsync(user);
    }
    
    private static string TryGetIdentityError(SignInResult result)
    {
        if (result.IsLockedOut)
        {
            return nameof(result.IsLockedOut);
        }

        if (result.IsNotAllowed)
        {
            return nameof(result.IsNotAllowed);
        }
        
        return "Неправильный логин или пароль";
    }
}
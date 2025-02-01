using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.AuthService.Dto;
using ObjectiveManager.Models.Result;

namespace AuthService.Application.Services.Interfaces;

public interface IAccountService
{
    public Task<Result<AccountData>> GetAccountDataAsync(string userId);
    public Task<Result<TokenCredentials>> RegisterUserAsync(RegisterDto model);
    public Task<Result> EditAccountAsync(string accountId, EditAccountDto model);
    public Task<Result<TokenCredentials>> LoginUserAsync(LoginViewModel model);
    public Task<Result<TokenCredentials>> RefreshToken(string userId);
}
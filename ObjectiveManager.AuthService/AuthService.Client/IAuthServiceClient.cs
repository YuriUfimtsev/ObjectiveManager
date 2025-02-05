using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.AuthService.Dto;
using ObjectiveManager.Models.Result;

namespace AuthService.Client;

public interface IAuthServiceClient
{
    public Task<Result<AccountData>> GetUserData(string userId = "");
    public Task<Result<TokenCredentials>> RegisterUser(RegisterDto model);
    public Task<Result<TokenCredentials>> LoginUser(LoginViewModel model);
    public Task<Result<TokenCredentials>> RefreshUserToken();
    public Task<Result> EditUser(EditAccountDto model);
}
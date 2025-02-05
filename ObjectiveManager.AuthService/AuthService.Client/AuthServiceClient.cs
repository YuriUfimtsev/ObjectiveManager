using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Models.AuthService.Dto;
using ObjectiveManager.Models.Result;
using ObjectiveManager.Utils.Http;

namespace AuthService.Client;

public class AuthServiceClient : IAuthServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri _authServiceUri;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthServiceClient(HttpClient httpClient,
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _authServiceUri = new Uri(configuration.GetSection("Services")["Auth"]);
    }
    
    public async Task<Result<AccountData>> GetUserData(string userId = "")
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _authServiceUri + $"api/Account/userData");
        
        if (userId != string.Empty)
        {
            httpRequest.AddUserIdToHeader(userId);
        }
        else
        {
            httpRequest.AddUserIdToHeader(_httpContextAccessor);
        }

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result<AccountData>>();
    }

    public async Task<Result<TokenCredentials>> RegisterUser(RegisterDto model)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            _authServiceUri + "api/Account/register");
        httpRequest.Content = new StringContent(
            JsonConvert.SerializeObject(model),
            Encoding.UTF8,
            "application/json");
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result<TokenCredentials>>();
    }
    
    public async Task<Result<TokenCredentials>> LoginUser(LoginViewModel model)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            _authServiceUri + "api/Account/login");
        httpRequest.Content = new StringContent(
            JsonConvert.SerializeObject(model),
            Encoding.UTF8,
            "application/json");
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result<TokenCredentials>>();
    }
    
    public async Task<Result<TokenCredentials>> RefreshUserToken()
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _authServiceUri + $"api/Account/refreshToken");
        httpRequest.AddUserIdToHeader(_httpContextAccessor);

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result<TokenCredentials>>();
    }
    
    public async Task<Result> EditUser(EditAccountDto model)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Put,
            _authServiceUri + $"api/Account/edit");
        httpRequest.Content = new StringContent(
            JsonConvert.SerializeObject(model),
            Encoding.UTF8,
            "application/json");
        httpRequest.AddUserIdToHeader(_httpContextAccessor);
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result>();
    }
}
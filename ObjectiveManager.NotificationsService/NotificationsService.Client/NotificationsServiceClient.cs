using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ObjectiveManager.Models.NotificationsService.DTO;
using ObjectiveManager.Models.Result;
using ObjectiveManager.Utils.Http;

namespace NotificationsService.Client;

public class NotificationsServiceClient : INotificationsServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri _objectivesServiceUri;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NotificationsServiceClient(HttpClient httpClient,
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _objectivesServiceUri = new Uri(configuration.GetSection("Services")["Notifications"]);
    }

    public async Task<Result<NotificationDTO>> GetNotification()
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _objectivesServiceUri + "api/Notifications");

        httpRequest.AddUserIdToHeader(_httpContextAccessor);

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result<NotificationDTO>>();
    }

    public async Task<string> CreateNotification(string userId)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            _objectivesServiceUri + $"api/Notifications?userId={userId}");
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<string>();
    }
    
    public async Task<Result> UpdateNotificationFrequency(string notificationId, long frequencyValueId)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Put,
            _objectivesServiceUri + $"api/Notifications/updateFrequency/{notificationId}?frequencyValueId={frequencyValueId}");
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result>();
    }
    
    public async Task<FrequencyValueDTO[]> GetAllFrequencyValues()
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _objectivesServiceUri + "api/Frequency/all");

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<FrequencyValueDTO[]>();
    }
}
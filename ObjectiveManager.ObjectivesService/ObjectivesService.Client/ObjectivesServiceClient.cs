using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ObjectiveManager.Models.ObjectivesService.DTO;
using ObjectiveManager.Models.Result;
using ObjectiveManager.Utils.Http;

namespace ObjectivesService.Client;

public class ObjectivesServiceClient : IObjectivesServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri _objectivesServiceUri;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ObjectivesServiceClient(HttpClient httpClient,
        IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _objectivesServiceUri = new Uri(configuration.GetSection("Services")["Objectives"]);
    }

    public async Task<ObjectiveDTO[]> GetAllObjectives()
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _objectivesServiceUri + "api/Objectives/all");

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<ObjectiveDTO[]>();
    }

    public async Task<Result<ObjectiveDTO>> GetObjectiveInfo(string objectiveId)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _objectivesServiceUri + $"api/Objectives/{objectiveId}");

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result<ObjectiveDTO>>();
    }

    public async Task<string> CreateObjective(ObjectivePostDto objective)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            _objectivesServiceUri + "api/Objectives");
        httpRequest.Content = new StringContent(
            JsonConvert.SerializeObject(objective),
            Encoding.UTF8,
            "application/json");

        httpRequest.AddUserIdToHeader(_httpContextAccessor);
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<string>();
    }

    public async Task<Result> DeleteObjective(string objectiveId)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Delete,
            _objectivesServiceUri + $"api/Objectives/{objectiveId}");

        httpRequest.AddUserIdToHeader(_httpContextAccessor);

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result>();
    }
    
    public async Task<Result> UpdateObjectiveInfo(string objectiveId, ObjectivePutDto objectivePutDto)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Put,
            _objectivesServiceUri + $"api/Objectives/updateInfo/{objectiveId}");
        httpRequest.Content = new StringContent(
            JsonConvert.SerializeObject(objectivePutDto),
            Encoding.UTF8,
            "application/json");
        
        httpRequest.AddUserIdToHeader(_httpContextAccessor);
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result>();
    }

    public async Task<Result> UpdateObjectiveStatus(string objectiveId, StatusObjectPutDTO updateObjectDto)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Put,
            _objectivesServiceUri + $"api/Objectives/updateStatusObject/{objectiveId}");
        httpRequest.Content = new StringContent(
            JsonConvert.SerializeObject(updateObjectDto),
            Encoding.UTF8,
            "application/json");
        
        httpRequest.AddUserIdToHeader(_httpContextAccessor);
        
        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<Result>();
    }
    
    public async Task<StatusObjectDTO[]> GetStatusesHistory(string objectiveId)
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _objectivesServiceUri + $"api/Objectives/statusHistory/{objectiveId}");

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<StatusObjectDTO[]>();
    }

    public async Task<StatusValueDTO[]> GetAllStatuses()
    {
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Get,
            _objectivesServiceUri + "api/Statuses/all");

        var response = await _httpClient.SendAsync(httpRequest);
        return await response.DeserializeAsync<StatusValueDTO[]>();
    }
}
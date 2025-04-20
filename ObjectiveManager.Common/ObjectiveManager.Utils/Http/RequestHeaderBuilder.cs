using Microsoft.AspNetCore.Http;

namespace ObjectiveManager.Utils.Http;

public static class RequestHeaderBuilder
{
    public static string UserIdHeader => "UserId";
    
    public static void AddUserIdToHeader(this HttpRequestMessage request,
        IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst("_id");
        if (userId is null)
        {
            throw new HttpRequestException($"HttpContext не содержит атрибута _id.");
        }
        
        request.Headers.Add(UserIdHeader, userId.Value);
    }
    
    public static void AddUserIdToHeader(this HttpRequestMessage request, string userId)
        =>request.Headers.Add(UserIdHeader, userId);
}
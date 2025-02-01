using Microsoft.AspNetCore.Http;

namespace ObjectiveManager.Utils.Http;

public static class HttpRequestExtensions
{
    public static string GetUserIdFromHeader(this HttpRequest request)
    {
        if (!request.Headers.TryGetValue(RequestHeaderBuilder.UserIdHeader, out var id)
            || string.IsNullOrEmpty(id.FirstOrDefault()))
        {
            var requestUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            throw new HttpRequestException(
                $"Запрос {requestUrl} не содержит заголовок UserId.");
        }

        return id.First()!;
    }
}
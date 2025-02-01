using Newtonsoft.Json;

namespace ObjectiveManager.Utils.Http;

public static class JsonExtensions
{
    public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Запрос {response.RequestMessage} завершился с кодом {(int)response.StatusCode} ({response.StatusCode}).");
        }

        var content = await response.Content.ReadAsStringAsync();
        var deserializedObject =  JsonConvert.DeserializeObject<T>(
            content,
            ObjectiveManagerJsonSerializerSettings.Settings);

        if (deserializedObject is null)
        {
            throw new HttpRequestException(
                $"В результате десериализации результата запроса {response.RequestMessage} получено значение null.");
        }
        
        return deserializedObject;
    }
}
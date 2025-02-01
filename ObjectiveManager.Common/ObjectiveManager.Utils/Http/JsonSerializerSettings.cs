using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ObjectiveManager.Utils.Http;

public class ObjectiveManagerJsonSerializerSettings
{
    static ObjectiveManagerJsonSerializerSettings()
    {
        Settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            Converters =
            {
                new StringEnumConverter(new CamelCaseNamingStrategy()),
            }
        };
    }

    public static JsonSerializerSettings? Settings { get; }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pactify
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PublishType
    {
        File,
        Http
    }
}

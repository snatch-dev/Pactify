using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Pactify.Serialization
{
    internal static class PactifySerialization
    {
        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include,
            Formatting = Formatting.Indented
        };
    }
}

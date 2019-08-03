using Newtonsoft.Json;

namespace Pactify.Definitions.Http
{
    internal sealed class HttpInteractionDefinition
    {
        [JsonProperty(PropertyName = "provider_state")]
        public string State { get; set; }
        public string Description { get; set; }
        public HttpPactRequest Request { get; set; }
        public HttpPactResponse Response { get; set; }
    }
}

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pactify.Definitions;
using Pactify.Serialization;

namespace Pactify.Publishers
{
    internal sealed class HttpPactPublisher : IPactPublisher
    {
        private const string RequestHeader = "Pact-Requester";

        private readonly string _url;
        private readonly HttpMethod _method;
        private readonly string _apiKey;

        public HttpPactPublisher(string url, HttpMethod method, string apiKey)
        {
            _url = url;
            _method = method;
            _apiKey = apiKey;
        }

        public async Task PublishAsync(PactDefinition definition)
        {
            var json = JsonConvert.SerializeObject(definition, PactifySerialization.Settings);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_url),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Method = _method
            };

            if (!(_apiKey is null))
            {
                request.Headers.Add(RequestHeader,_apiKey);
            }

            await new HttpClient().SendAsync(request);
        }
    }
}

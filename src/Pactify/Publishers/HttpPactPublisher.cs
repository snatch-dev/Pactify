using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pactify.Definitions;

namespace Pactify.Publishers
{
    internal sealed class HttpPactPublisher : IPactPublisher
    {
        private const string RequestHeader = "Pact-Requester";

        private readonly string _url;
        private readonly string _apiKey;

        public HttpPactPublisher(string url, string apiKey)
        {
            _url = url;
            _apiKey = apiKey;
        }

        public async Task PublishAsync(PactDefinition definition)
        {
            var httpClient = new HttpClient();

            if (!(_apiKey is null))
            {
                httpClient.DefaultRequestHeaders.Add(RequestHeader,_apiKey);
            }

            var json = JsonConvert.SerializeObject(definition);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync(_url, stringContent);
        }
    }
}

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pactify.Definitions;

namespace Pactify.Retrievers
{
    internal sealed class HttpPactRetriever : IPactRetriever
    {
        private const string RequestHeader = "Pact-Requester";

        private readonly string _url;
        private readonly string _apiKey;

        public HttpPactRetriever(string url, string apiKey)
        {
            _url = url;
            _apiKey = apiKey;
        }

        public async Task<PactDefinition> RetrieveAsync()
        {
            var httpClient = new HttpClient();

            if (!(_apiKey is null))
            {
                httpClient.DefaultRequestHeaders.Add(RequestHeader,_apiKey);
            }

            var response = await httpClient.GetAsync(_url);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PactDefinition>(json);
        }
    }
}

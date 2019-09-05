using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Pactify.Messages;
using Pactify.Retrievers;
using Pactify.Verifiers;

namespace Pactify
{
    public sealed class PactVerifier : IPactVerifier
    {
        private readonly HttpClient _httpClient;
        private readonly object _pactTemplateObject;
        private string _consumer;
        private string _provider;
        private IPactRetriever _retriever;

        private PactVerifier(HttpClient httpClient, object pactTemplateObject)
        {
            _httpClient = httpClient;
            _pactTemplateObject = pactTemplateObject;
        }

        public static IPactVerifier Create(HttpClient httpClient = null, 
            object pactTemplateObject = null)
            => new PactVerifier(httpClient ?? new HttpClient(), pactTemplateObject);

        public static IPactVerifier CreateFor<TStartup>(object pactTemplateObject = null) where TStartup : class
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup<TStartup>());
            var httpClient = testServer.CreateClient();
            return Create(httpClient, pactTemplateObject);
        }

        public IPactVerifier Between(string consumer, string provider)
        {
            _consumer = consumer;
            _provider = provider;

            return this;
        }

        public IPactVerifier RetrievedFromFile(string localPath)
        {
            if (string.IsNullOrEmpty(_consumer) || string.IsNullOrEmpty(_provider))
            {
                throw new PactifyException(ErrorMessages.ConsumerProviderMustBeDefined);
            }

            _retriever = new FilePactRetriever(_consumer, _provider, localPath);
            return this;
        }

        public IPactVerifier RetrievedViaHttp(string url, string apiKey = null)
        {
            _retriever = new HttpPactRetriever(url, apiKey);
            return this;
        }

        public void Verify()
            => VerifyAsync().GetAwaiter().GetResult();

        public async Task VerifyAsync()
        {
            var definition = await _retriever.RetrieveAsync();
            var verifier = new HttpInteractionVerifier(_httpClient);

            var resultTasks = definition.Interactions
                .Select(c => verifier.VerifyAsync(c, definition.Options, _pactTemplateObject))
                .ToList();

            await Task.WhenAll(resultTasks);

            var result = resultTasks
                .Select(t => t.Result)
                .Aggregate((c, next) => c & next);

            if (result.IsSuccessful)
            {
                return;
            }

            throw new PactifyException(string.Join(Environment.NewLine, result.Errors));
        }
    }
}

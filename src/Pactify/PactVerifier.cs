using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Pactify.Retrievers;
using Pactify.Verifiers;

namespace Pactify
{
    public sealed class PactVerifier : IPactVerifier
    {
        private readonly HttpClient _httpClient;
        private string _consumer;
        private string _provider;
        private IPactRetriever _retriever;

        private Func<Task> _onBefore = () => Task.CompletedTask;
        private Func<Task> _onAfter = () => Task.CompletedTask;



        private PactVerifier(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static IPactVerifier Create(HttpClient httpClient = null)
            => new PactVerifier(httpClient ?? new HttpClient());

        public static IPactVerifier CreateFor<TStartup>() where TStartup : class
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup<TStartup>());
            var httpClient = testServer.CreateClient();
            return Create(httpClient);
        }

        public IPactVerifier Between(string consumer, string provider)
        {
            if (string.IsNullOrEmpty(consumer) || string.IsNullOrEmpty(provider))
            {
                throw new PactifyException("Both consumer and provider must be defined");
            }

            _consumer = consumer;
            _provider = provider;

            return this;
        }

        public IPactVerifier RetrievedFromFile(string localPath)
        {
            _retriever = new FilePactRetriever(_consumer, _provider, localPath);
            return this;
        }

        public IPactVerifier RetrievedViaHttp(string url, string apiKey = null)
        {
            _retriever = null;
            return this;
        }

        public IPactVerifier After(Func<Task> onAfter)
        {
            _ = onAfter ?? throw new PactifyException("'After' hook mast be provided");
            _onAfter = onAfter;
            return this;
        }

        public async Task VerifyAsync()
        {
            await _onBefore();

            var definition = _retriever.Retrieve();
            var verifier = new HttpCouplingVerifier(_httpClient);

            var resultTasks = definition.Couplings
                .Select(c => verifier.VerifyAsync(c, definition.Options))
                .ToList();

            await Task.WhenAll(resultTasks);

            var result = resultTasks
                .Select(t => t.Result)
                .Aggregate((c, next) => c & next);

            await _onAfter();

            if (result.IsSuccessful)
            {
                return;
            }

            throw new PactifyException(string.Join(Environment.NewLine, result.Errors));
        }

        public void Verify()
            => VerifyAsync().GetAwaiter().GetResult();
    }
}

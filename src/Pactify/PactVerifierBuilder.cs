using System.Net.Http;
using Pactify.Retrievers;
using Pactify.Verifiers;

namespace Pactify
{
    public sealed class PactVerifierBuilder : IPactVerifierBuilder
    {
        private readonly PactDefinitionOptions _options;
        private readonly IPactRetrieverFactory _factory;

        private string _consumer;
        private string _provider;

        private HttpClient _httpClient;

        private PactVerifierBuilder(PactDefinitionOptions options, IPactRetrieverFactory factory)
        {
            _options = options;
            _factory = factory;
        }

        public static IPactVerifierBuilder Create(PactDefinitionOptions options)
            => new PactVerifierBuilder(options, new PactRetrieverFactory());

        public IPactVerifierBuilder Between(string consumer, string provider)
        {
            if (string.IsNullOrEmpty(consumer) || string.IsNullOrEmpty(provider))
            {
                throw new PactifyException("Both consumer and provider must be defined");
            }

            _consumer = consumer;
            _provider = provider;

            return this;
        }

        public IPactVerifierBuilder UsingHttp(HttpClient httpClient)
        {
            _httpClient = httpClient ?? new HttpClient();
            return this;
        }

        public IPactVerifier Build()
        {
            var dispatcher = new CouplingVerifierDispatcher(_httpClient);
            var retriever = _factory.Create(_options.PublishType, _consumer, _provider);

            if (retriever is null)
            {
                throw new PactifyException("Given publish type is invalid");
            }

            return new PactVerifier(_options, retriever, dispatcher);
        }
    }
}

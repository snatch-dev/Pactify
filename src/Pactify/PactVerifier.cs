using System;
using System.Linq;
using System.Net.Http;
using Pactify.Retrievers;
using Pactify.Verifiers;

namespace Pactify
{
    public class PactVerifier : IPactVerifier
    {
        private readonly PactDefinitionOptions _options;
        private readonly IPactRetrieverFactory _factory;
        private readonly ICouplingVerifierDispatcher _dispatcher;

        private string _consumer;
        private string _provider;

        private HttpClient _httpClient;

        private PactVerifier(PactDefinitionOptions options, IPactRetrieverFactory factory,
            ICouplingVerifierDispatcher dispatcher)
        {
            _options = options;
            _factory = factory;
            _dispatcher = dispatcher;
        }

        public static IPactVerifier Create(PactDefinitionOptions options)
            => new PactVerifier(options, new PactRetrieverFactory(), null);

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

        public IPactVerifier UsingHttp(HttpClient httpClient)
        {
            _httpClient = httpClient ?? new HttpClient();
            return this;
        }

        public void Verify()
        {
            var retriever = _factory.Create(_options.PublishType);

            if (retriever is null)
            {
                throw new PactifyException("Provided Publish type is invalid");
            }

            var definition = retriever.Retrieve(_consumer, _provider, _options);

            var result = definition.Couplings
                .Select(_dispatcher.Dispatch)
                .Aggregate((c, next) => c & next);

            if (result.IsSuccessful)
            {
                return;
            }

            throw new PactifyException(string.Join(Environment.NewLine, result.Errors));
        }
    }
}

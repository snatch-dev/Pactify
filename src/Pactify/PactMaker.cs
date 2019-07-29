using System;
using Pactify.Builders;
using Pactify.Builders.Http;
using Pactify.Definitions;
using Pactify.Definitions.Http;
using Pactify.Publishers;

namespace Pactify
{
    public class PactMaker : IPactMaker
    {
        private readonly PactDefinition _pactDefinition;
        private IPactPublisher _publisher;

        private PactMaker(PactDefinitionOptions options)
        {
            _pactDefinition = new PactDefinition
            {
                Options = options
            };
        }

        public static IPactMaker Create(PactDefinitionOptions options)
        {
            if (options is null)
            {
                throw new PactifyException("Options must be provided");
            }

            return new PactMaker(options);
        }

        public IPactMaker Between(string consumer, string provider)
        {
            if (string.IsNullOrEmpty(consumer) || string.IsNullOrEmpty(provider))
            {
                throw new PactifyException("Both consumer and provider must be defined");
            }

            _pactDefinition.Consumer = new ConsumerDefinition { Name = consumer };
            _pactDefinition.Provider = new ProviderDefinition { Name = provider };
            return this;
        }

        public IPactMaker WithHttpCoupling(Action<IHttpCouplingBuilder> buildCoupling)
        {
            if (buildCoupling is null)
            {
                throw new PactifyException("Coupling definition must be defined");
            }

            var builder = new HttpCouplingBuilder();
            buildCoupling(builder);

            var accessor = (IBuildingAccessor<HttpCouplingDefinition>)builder;
            var definition = accessor.Build();
            _pactDefinition.Couplings.Add(definition);

            return this;
        }

        public IPactMaker PublishedAsFile(string localPath)
        {
            _publisher = new FilePactPublisher(localPath);
            return this;
        }

        public IPactMaker PublishedViaHttp(string url, string apiKey = null)
        {
            throw new NotImplementedException();
        }

        public void Make()
        {
            if (_publisher is null)
            {
                throw new PactifyException("PACT publisher has not been set up");
            }

            _publisher.Publish(_pactDefinition);
        }
    }
}

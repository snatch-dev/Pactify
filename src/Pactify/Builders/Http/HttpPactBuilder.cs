using System;
using System.IO;
using Newtonsoft.Json;
using Pactify.Definitions;
using Pactify.Definitions.Http;
using Pactify.Publishers;

namespace Pactify.Builders.Http
{
    internal sealed class HttpPactBuilder : IHttpPactBuilder
    {
        private readonly PactDefinition _pactDefinition;
        private readonly IPublisherFactory _factory;

        public HttpPactBuilder(PactDefinitionOptions options, IPublisherFactory factory)
        {
            _pactDefinition = new PactDefinition
            {
                Options = options
            };

            _factory = factory;
        }

        public IHttpPactBuilder Between(string consumer, string provider)
        {
            if (string.IsNullOrEmpty(consumer) || string.IsNullOrEmpty(provider))
            {
                throw new PactifyException("Both consumer and provider must be defined");
            }
            
            _pactDefinition.Consumer = new ConsumerDefinition { Name = consumer };
            _pactDefinition.Provider = new ProviderDefinition { Name = provider };
            return this;
        }

        public IHttpPactBuilder WithCoupling(Action<IHttpCouplingBuilder> buildCoupling)
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

        public void Make()
        {
            var publisher = _factory.Create(_pactDefinition.Options.PublishType);

            if (publisher is null)
            {
                throw new PactifyException("Provided Publish type is invalid");
            }

            publisher.Publish(_pactDefinition);
        }
    }
}

using System;
using Pactify.Builders;
using Pactify.Builders.Http;
using Pactify.Definitions;
using Pactify.Definitions.Http;
using Pactify.Publishers;

namespace Pactify
{
    public class PactBuilder : IPactBuilder
    {
        private readonly PactDefinition _pactDefinition;
        private readonly IPactPublisherFactory _factory;

        private PactBuilder(PactDefinitionOptions options, IPactPublisherFactory factory)
        {
            _pactDefinition = new PactDefinition
            {
                Options = options
            };

            _factory = factory;
        }
        
        public static IPactBuilder Create(PactDefinitionOptions options)
        {
            if (options is null)
            {
                throw new PactifyException("Options must be provided");
            }
            
            return new PactBuilder(options, new PactPublisherFactory());
        }

        public IPactBuilder Between(string consumer, string provider)
        {
            if (string.IsNullOrEmpty(consumer) || string.IsNullOrEmpty(provider))
            {
                throw new PactifyException("Both consumer and provider must be defined");
            }
            
            _pactDefinition.Consumer = new ConsumerDefinition { Name = consumer };
            _pactDefinition.Provider = new ProviderDefinition { Name = provider };
            return this;
        }

        public IPactBuilder WithHttpCoupling(Action<IHttpCouplingBuilder> buildCoupling)
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

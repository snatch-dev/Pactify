using System;
using Pactify.Definitions;
using Pactify.Definitions.Http;

namespace Pactify.Builders.Http
{
    internal sealed class HttpPactBuilder : IHttpPactBuilder
    {
        private readonly PactDefinition _pactDefinition;

        public HttpPactBuilder()
        {
            _pactDefinition = new PactDefinition();
        }

        public IHttpPactBuilder Between(string consumer, string provider)
        {
            if (string.IsNullOrEmpty(consumer) || string.IsNullOrEmpty(provider))
            {
                throw new PactifyException("Both consumer and provider must be defined");
            }
            
            _pactDefinition.Consumer = new ConsumerDefinition { Name = consumer };
            _pactDefinition.Provider = new ProviderDefinition { Name = consumer };
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
            throw new NotImplementedException();
        }
    }
}

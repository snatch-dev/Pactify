using System;
using Pactify.Definitions.Http;

namespace Pactify.Builders.Http
{
    internal sealed class HttpCouplingBuilder : IHttpCouplingBuilder, IBuildingAccessor<HttpCouplingDefinition>
    {
        private readonly HttpCouplingDefinition _definition;

        public HttpCouplingBuilder()
        {
            _definition = new HttpCouplingDefinition();
        }
        
        public IHttpCouplingBuilder Given(string state)
        {
            if (string.IsNullOrEmpty(state))
            {
                throw new PactifyException("'Given' must be provided");
            }

            _definition.State = state;
            return this;
        }

        public IHttpCouplingBuilder UponReceiving(string description)
        {
            _definition.Description = description;
            return this;
        }

        public IHttpCouplingBuilder With(Action<IHttpPactRequestBuilder> buildRequest)
        {
            if (buildRequest is null)
            {
                throw new PactifyException("'With' must be provided");
            }
            var builder = new HttpPactRequestBuilder();
            buildRequest(builder);

            var accessor = (IBuildingAccessor<HttpPactRequest>)builder;
            _definition.Request = accessor.Build();

            return this;
        }

        public IHttpCouplingBuilder WillRespondWith(Action<IHttpPactResponseBuilder> buildResponse)
        {
            if (buildResponse is null)
            {
                throw new PactifyException("'WillRespondWith' must be provided");
            }
            var builder = new HttpPactResponseBuilder();
            buildResponse(builder);

            var accessor = (IBuildingAccessor<HttpPactResponse>)builder;
            _definition.Response = accessor.Build();

            return this;
        }

        public HttpCouplingDefinition Build()
            => _definition;
    }
}

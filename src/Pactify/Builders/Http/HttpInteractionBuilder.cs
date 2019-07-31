using System;
using Pactify.Definitions.Http;

namespace Pactify.Builders.Http
{
    internal sealed class HttpInteractionBuilder : IHttpInteractionBuilder, IBuildingAccessor<HttpInteractionDefinition>
    {
        private readonly HttpInteractionDefinition _definition;

        public HttpInteractionBuilder()
        {
            _definition = new HttpInteractionDefinition();
        }

        public IHttpInteractionBuilder Given(string state)
        {
            if (string.IsNullOrEmpty(state))
            {
                throw new PactifyException("'Given' must be provided");
            }

            _definition.State = state;
            return this;
        }

        public IHttpInteractionBuilder UponReceiving(string description)
        {
            _definition.Description = description;
            return this;
        }

        public IHttpInteractionBuilder With(Action<IHttpPactRequestBuilder> buildRequest)
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

        public IHttpInteractionBuilder WillRespondWith(Action<IHttpPactResponseBuilder> buildResponse)
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

        public HttpInteractionDefinition Build()
            => _definition;
    }
}

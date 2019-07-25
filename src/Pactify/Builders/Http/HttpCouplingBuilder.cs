using System;
using Pactify.Definitions.Http;

namespace Pactify.Builders.Http
{
    internal sealed class HttpCouplingBuilder : IHttpCouplingBuilder, IBuildingAccessor<HttpCouplingDefinition>
    {
        public IHttpCouplingBuilder Given(string state)
        {
            throw new NotImplementedException();
        }

        public IHttpCouplingBuilder UponReceiving(string description)
        {
            throw new NotImplementedException();
        }

        public IHttpCouplingBuilder With(Action<IHttpPactRequestBuilder> buildRequest)
        {
            throw new NotImplementedException();
        }

        public IHttpCouplingBuilder WillRespondWith(Action<IHttpPactResponseBuilder> buildResponse)
        {
            throw new NotImplementedException();
        }

        public HttpCouplingDefinition Build()
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace Pactify
{
    public interface IHttpCouplingBuilder
    {
        IHttpCouplingBuilder Given(string state);
        IHttpCouplingBuilder UponReceiving(string description);
        IHttpCouplingBuilder With(Action<IHttpPactRequestBuilder> buildRequest);
        IHttpCouplingBuilder WillRespondWith(Action<IHttpPactResponseBuilder> buildResponse);
    }
}

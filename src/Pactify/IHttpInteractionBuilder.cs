using System;

namespace Pactify
{
    public interface IHttpInteractionBuilder
    {
        IHttpInteractionBuilder Given(string state);
        IHttpInteractionBuilder UponReceiving(string description);
        IHttpInteractionBuilder With(Action<IHttpPactRequestBuilder> buildRequest);
        IHttpInteractionBuilder WillRespondWith(Action<IHttpPactResponseBuilder> buildResponse);
    }
}

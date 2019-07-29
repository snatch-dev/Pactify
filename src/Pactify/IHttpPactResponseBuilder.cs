using System.Net;

namespace Pactify
{
    public interface IHttpPactResponseBuilder
    {
        IHttpPactResponseBuilder WithHeader(string key, string value);
        IHttpPactResponseBuilder WithStatusCode(HttpStatusCode statusCode);
        IHttpPactResponseBuilder WithBody(object body);
        IHttpPactResponseBuilder WithBody<TBody>();
    }
}

using System.Net.Http;

namespace Pactify
{
    public interface IHttpPactRequestBuilder
    {
        IHttpPactRequestBuilder WithMethod(HttpMethod method);
        IHttpPactRequestBuilder WithPath(string path);
    }
}

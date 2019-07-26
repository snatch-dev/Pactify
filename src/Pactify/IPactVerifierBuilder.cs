using System.Net.Http;

namespace Pactify
{
    public interface IPactVerifierBuilder
    {
        IPactVerifierBuilder Between(string consumer, string provider);
        IPactVerifierBuilder UsingHttp(HttpClient httpClient);
        IPactVerifier Build();
    }
}

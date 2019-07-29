using System.Net.Http;

namespace Pactify
{
    public interface IPactVerifierBuilder
    {
        IPactVerifierBuilder Between(string consumer, string provider);
        IPactVerifierBuilder UsingHttpClient(HttpClient httpClient);
        IPactVerifier Build();
    }
}

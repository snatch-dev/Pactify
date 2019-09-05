using System.Net.Http;
using System.Threading.Tasks;

namespace Pactify
{
    public interface IPactVerifier
    {
        IPactVerifier Between(string consumer, string provider);
        IPactVerifier UsePathTemplateObject(object pathTemplateObject);
        IPactVerifier RetrievedFromFile(string localPath);
        IPactVerifier RetrievedViaHttp(string url, string apiKey = null);
        Task VerifyAsync();
        void Verify();
    }
}

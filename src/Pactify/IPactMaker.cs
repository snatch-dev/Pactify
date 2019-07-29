using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pactify
{
    public interface IPactMaker
    {
        IPactMaker Between(string consumer, string provider);
        IPactMaker WithHttpCoupling(Action<IHttpCouplingBuilder> buildCoupling);
        IPactMaker PublishedAsFile(string localPath);
        IPactMaker PublishedViaHttp(string url, HttpMethod method, string apiKey = null);
        void Make();
        Task MakeAsync();
    }
}

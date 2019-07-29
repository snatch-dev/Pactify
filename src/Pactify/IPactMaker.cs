using System;

namespace Pactify
{
    public interface IPactMaker
    {
        IPactMaker Between(string consumer, string provider);
        IPactMaker WithHttpCoupling(Action<IHttpCouplingBuilder> buildCoupling);
        IPactMaker PublishedAsFile(string localPath);
        IPactMaker PublishedViaHttp(string url, string apiKey = null);
        void Make();
    }
}

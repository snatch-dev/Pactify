using System;

namespace Pactify
{
    public interface IPactBuilder
    {
        IPactBuilder Between(string consumer, string provider);
        IPactBuilder WithHttpCoupling(Action<IHttpCouplingBuilder> buildCoupling);
        void Make();
    }
}

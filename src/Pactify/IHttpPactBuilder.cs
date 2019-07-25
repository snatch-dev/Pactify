using System;

namespace Pactify
{
    public interface IHttpPactBuilder
    {
        IHttpPactBuilder Between(string consumer, string provider);
        IHttpPactBuilder WithCoupling(Action<IHttpCouplingBuilder> buildCoupling);
        void Make();
    }
}

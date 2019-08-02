using System;

namespace Pactify
{
    public interface IHttpPactBuilder
    {
        IHttpPactBuilder Between(string consumer, string provider);
        IHttpPactBuilder WithCoupling(Action<IHttpInteractionBuilder> buildCoupling);
        void Make();
    }
}

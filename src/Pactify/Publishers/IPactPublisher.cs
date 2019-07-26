using Pactify.Definitions;

namespace Pactify.Publishers
{
    internal interface IPactPublisher
    {
        void Publish(PactDefinition definition);
    }
}

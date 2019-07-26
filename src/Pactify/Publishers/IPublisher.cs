using Pactify.Definitions;

namespace Pactify.Publishers
{
    internal interface IPublisher
    {
        void Publish(PactDefinition definition);
    }
}

namespace Pactify.Publishers
{
    internal interface IPactPublisherFactory
    {
        IPactPublisher Create(PublishType type);
    }
}

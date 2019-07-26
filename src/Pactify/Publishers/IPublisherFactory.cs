namespace Pactify.Publishers
{
    internal interface IPublisherFactory
    {
        IPublisher Create(PublishType type);
    }
}

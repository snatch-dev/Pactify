namespace Pactify.Publishers
{
    internal sealed class PublisherFactory : IPublisherFactory
    {
        public IPublisher Create(PublishType type)
        {
            switch (type)
            {
                case PublishType.File:
                    return new FilePublisher();
                case PublishType.Http:
                default:
                    return null;
            }
        }
    }
}

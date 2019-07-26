namespace Pactify.Publishers
{
    internal sealed class PactPublisherFactory : IPactPublisherFactory
    {
        public IPactPublisher Create(PublishType type)
        {
            switch (type)
            {
                case PublishType.File:
                    return new FilePactPublisher();
                case PublishType.Http:
                default:
                    return null;
            }
        }
    }
}

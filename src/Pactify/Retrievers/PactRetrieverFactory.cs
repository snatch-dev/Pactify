namespace Pactify.Retrievers
{
    internal class PactRetrieverFactory : IPactRetrieverFactory
    {
        public IPactRetriever Create(PublishType type, string consumer, string provider)
        {
            switch (type)
            {
                case PublishType.File:
                    return new FilePactRetriever(consumer, provider);
                case PublishType.Http:
                default:
                    return null;
            }
        }
    }
}

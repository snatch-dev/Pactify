namespace Pactify.Retrievers
{
    internal class PactRetrieverFactory : IPactRetrieverFactory
    {
        public IPactRetriever Create(PublishType type)
        {
            switch (type)
            {
                case PublishType.File:
                    return new FilePactRetriever();
                case PublishType.Http:
                default:
                    return null;
            }
        }
    }
}

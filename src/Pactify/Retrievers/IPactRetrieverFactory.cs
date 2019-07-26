namespace Pactify.Retrievers
{
    internal interface IPactRetrieverFactory
    {
        IPactRetriever Create(PublishType type, string consumer, string provider);
    }
}

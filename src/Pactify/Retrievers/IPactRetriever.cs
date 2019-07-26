using Pactify.Definitions;

namespace Pactify.Retrievers
{
    internal interface IPactRetriever
    {
        PactDefinition Retrieve(string consumer, string provider, PactDefinitionOptions options);
    }
}

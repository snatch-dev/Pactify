using Pactify.Definitions;

namespace Pactify.Retrievers
{
    internal interface IPactRetriever
    {
        PactDefinition Retrieve(PactDefinitionOptions options);
    }
}

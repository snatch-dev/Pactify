using System.Threading.Tasks;
using Pactify.Definitions;

namespace Pactify.Retrievers
{
    internal interface IPactRetriever
    {
        Task<PactDefinition> RetrieveAsync();
    }
}

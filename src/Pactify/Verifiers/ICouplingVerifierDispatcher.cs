using System.Threading.Tasks;
using Pactify.Definitions;

namespace Pactify.Verifiers
{
    internal interface ICouplingVerifierDispatcher
    {
        Task<PactVerificationResult> DispatchAsync(ICouplingDefinition definition, PactDefinitionOptions options);
    }
}

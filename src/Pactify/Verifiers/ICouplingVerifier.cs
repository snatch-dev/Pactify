using System.Threading.Tasks;
using Pactify.Definitions;

namespace Pactify.Verifiers
{
    internal interface ICouplingVerifier
    {
        Task<PactVerificationResult> VerifyAsync(ICouplingDefinition definition, PactDefinitionOptions options);
    }
}

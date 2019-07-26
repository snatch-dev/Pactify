using Pactify.Definitions;

namespace Pactify.Verifiers
{
    internal interface ICouplingVerifierDispatcher
    {
        PactVerificationResult Dispatch(ICouplingDefinition definition);
    }
}

using System.Net.Http;
using System.Threading.Tasks;
using Pactify.Definitions;
using Pactify.Enums;

namespace Pactify.Verifiers
{
    internal sealed class CouplingVerifierDispatcher : ICouplingVerifierDispatcher
    {
        private HttpClient _httpClient;

        public CouplingVerifierDispatcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<PactVerificationResult> DispatchAsync(ICouplingDefinition definition, PactDefinitionOptions options)
        {
            switch (definition.Type)
            {
                case CouplingType.Http:
                    return new HttpCouplingVerifier(_httpClient).VerifyAsync(definition, options);
                case CouplingType.Other:
                default:
                    return null;
            }
        }
    }
}

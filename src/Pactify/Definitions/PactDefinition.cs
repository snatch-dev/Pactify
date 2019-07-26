using System.Collections.Generic;
using Newtonsoft.Json;
using Pactify.Converters;
using Pactify.Definitions.Http;

namespace Pactify.Definitions
{
    internal class PactDefinition
    {
        public ConsumerDefinition Consumer { get; set; }
        public ProviderDefinition Provider { get; set; }
        public List<HttpCouplingDefinition> Couplings { get; set; } = new List<HttpCouplingDefinition>();
        public PactDefinitionOptions Options { get; set; }
    }
}

using System.Collections.Generic;

namespace Pactify.Definitions
{
    internal class PactDefinition
    {
        public ConsumerDefinition Consumer { get; set; }
        public ProviderDefinition Provider { get; set; }
        public ISet<ICouplingDefinition> Couplings { get; set; } = new HashSet<ICouplingDefinition>();
        public PactDefinitionOptions Options { get; set; }
    }
}

using Pactify.Enums;

namespace Pactify.Definitions
{
    internal abstract class CouplingDefinitionBase : ICouplingDefinition
    {
        public CouplingType Type { get; set; }
        public string State { get; set; }
        public string Description { get; set; }

        protected CouplingDefinitionBase(CouplingType type)
        {
            Type = type;
        }
    }
}

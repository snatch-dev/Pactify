using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pactify.Enums;

namespace Pactify.Definitions
{
    internal class CouplingDefinitionBase : ICouplingDefinition
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CouplingType Type { get; set; }
        public string State { get; set; }
        public string Description { get; set; }

        protected CouplingDefinitionBase(CouplingType type)
        {
            Type = type;
        }
    }
}

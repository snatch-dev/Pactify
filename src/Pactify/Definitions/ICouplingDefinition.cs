using Pactify.Enums;

namespace Pactify.Definitions
{
    internal interface ICouplingDefinition
    {
        CouplingType Type { get; set; }
        string State { get; set; }
        string Description { get; set; }
    }    
}

using Pactify.Enums;

namespace Pactify.Definitions.Http
{
    internal class HttpCouplingDefinition : CouplingDefinitionBase
    {
        public HttpPactRequest Request { get; set; }
        public HttpPactResponse Response { get; set; }
        
        public HttpCouplingDefinition() : base(CouplingType.Http)
        {
        }
    }
}

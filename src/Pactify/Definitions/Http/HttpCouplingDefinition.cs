namespace Pactify.Definitions.Http
{
    internal class HttpCouplingDefinition
    {
        public string State { get; set; }
        public string Description { get; set; }
        public HttpPactRequest Request { get; set; }
        public HttpPactResponse Response { get; set; }
    }
}

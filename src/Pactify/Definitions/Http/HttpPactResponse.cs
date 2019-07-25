using System.Collections.Generic;
using System.Net;

namespace Pactify.Definitions.Http
{
    public class HttpPactResponse
    {
        public IDictionary<string, string> Headers { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Body { get; set; }
    }
}

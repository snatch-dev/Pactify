using System.Net;
using System.Net.Http;

namespace Pactify.Definitions.Http
{
    internal class HttpPactRequest
    {
        public HttpMethod Method { get; set; }
        public string Path { get; set; }
    }
}

using System.Net.Http;
using Pactify.Definitions.Http;

namespace Pactify.Builders.Http
{
    internal sealed class HttpPactRequestBuilder : IHttpPactRequestBuilder, IBuildingAccessor<HttpPactRequest>
    {
        private readonly HttpPactRequest _pactRequest;

        public HttpPactRequestBuilder()
        {
            _pactRequest = new HttpPactRequest();
        }

        public IHttpPactRequestBuilder WithMethod(HttpMethod method)
        {
            _pactRequest.Method = method.Method;
            return this;
        }

        public IHttpPactRequestBuilder WithPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new PactifyException("HTTP path must be provided");
            }

            _pactRequest.Path = path;
            return this;
        }

        public HttpPactRequest Build()
            => _pactRequest;
    }
}

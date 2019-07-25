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
        
        public IHttpPactRequestBuilder WithMethod(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new PactifyException("HTTP method must be provided");
            }

            _pactRequest.Method = method;
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

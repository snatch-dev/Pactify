using Pactify.Builders;
using Pactify.Builders.Http;

namespace Pactify
{
    public static class PactBuilder
    {
        public static IHttpPactBuilder CreateForHttp()
            => new HttpPactBuilder();
    }
}

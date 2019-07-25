using Pactify.Builders.Http;
using Pactify.Definitions;

namespace Pactify
{
    public static class PactBuilder
    {
        public static IHttpPactBuilder CreateForHttp(PactDefinitionOptions options)
        {
            if (options is null)
            {
                throw new PactifyException("Options must be provided");
            }
            
            return new HttpPactBuilder(options);
        }
    }
}

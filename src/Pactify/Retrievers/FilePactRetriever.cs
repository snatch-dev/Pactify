using System.IO;
using Newtonsoft.Json;
using Pactify.Definitions;
using Pactify.Utils;

namespace Pactify.Retrievers
{
    internal sealed class FilePactRetriever : IPactRetriever
    {
        public readonly string _consumer;
        public readonly string _provider;

        public FilePactRetriever(string consumer, string provider)
        {
            _consumer = consumer;
            _provider = provider;
        }

        public PactDefinition Retrieve(PactDefinitionOptions options)
        {
            var path = PactifyUtils.CreatePactFilePath(_consumer, _provider, options);

            using (var reader = new StreamReader(path))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<PactDefinition>(json);
            }
        }
    }
}

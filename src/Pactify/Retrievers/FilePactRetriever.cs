using System.IO;
using Newtonsoft.Json;
using Pactify.Definitions;
using Pactify.Utils;

namespace Pactify.Retrievers
{
    internal sealed class FilePactRetriever : IPactRetriever
    {
        public PactDefinition Retrieve(string consumer, string provider, PactDefinitionOptions options)
        {
            var path = PactifyUtils.CreatePactFilePath(consumer, provider, options);

            using (var reader = new StreamReader(path))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<PactDefinition>(json);
            }
        }
    }
}

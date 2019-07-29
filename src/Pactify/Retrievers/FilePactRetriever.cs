using System.IO;
using Newtonsoft.Json;
using Pactify.Definitions;
using Pactify.Utils;

namespace Pactify.Retrievers
{
    internal sealed class FilePactRetriever : IPactRetriever
    {
        private readonly string _consumer;
        private readonly string _provider;
        private readonly string _localPath;

        public FilePactRetriever(string consumer, string provider, string localPath)
        {
            _consumer = consumer;
            _provider = provider;
            _localPath = localPath;
        }

        public PactDefinition Retrieve()
        {
            var path = PactifyUtils.CreatePactFilePath(_consumer, _provider, _localPath);

            using (var reader = new StreamReader(path))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<PactDefinition>(json);
            }
        }
    }
}

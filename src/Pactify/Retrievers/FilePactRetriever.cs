using System.IO;
using System.Threading.Tasks;
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

        public async Task<PactDefinition> RetrieveAsync()
        {
            var path = PactifyUtils.CreatePactFilePath(_consumer, _provider, _localPath);

            using (var reader = new StreamReader(path))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<PactDefinition>(json);
            }
        }
    }
}

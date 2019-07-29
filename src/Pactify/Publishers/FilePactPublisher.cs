using System.IO;
using Newtonsoft.Json;
using Pactify.Definitions;
using Pactify.Utils;

namespace Pactify.Publishers
{
    internal sealed class FilePactPublisher : IPactPublisher
    {
        private readonly string _localPath;

        public FilePactPublisher(string localPath)
        {
            _localPath = localPath;
        }

        public void Publish(PactDefinition definition)
        {
            Directory.CreateDirectory(_localPath);
            var filePath = PactifyUtils.CreatePactFilePath(definition, _localPath);

            using (var file = File.CreateText(filePath))
            {
                var json = JsonConvert.SerializeObject(definition, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.Indented
                });

                file.Write(json);
            }
        }
    }
}

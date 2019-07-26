using System.IO;
using Newtonsoft.Json;
using Pactify.Definitions;
using Pactify.Utils;

namespace Pactify.Publishers
{
    internal sealed class FilePactPublisher : IPactPublisher
    {
        public void Publish(PactDefinition definition)
        {
            Directory.CreateDirectory(definition.Options.DestinationPath);
            var filePath = PactifyUtils.CreatePactFilePath(definition);

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

using System.IO;
using Newtonsoft.Json;
using Pactify.Definitions;

namespace Pactify.Publishers
{
    internal sealed class FilePublisher : IPublisher
    {
        public void Publish(PactDefinition definition)
        {
            Directory.CreateDirectory(definition.Options.DestinationPath);
            var filePath = CreatePactFilePath(definition);

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

        private static string CreatePactFilePath(PactDefinition definition)
            => $"{definition.Options.DestinationPath}/{definition.Consumer.Name}-{definition.Provider.Name}.json";
    }
}

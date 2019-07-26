using Pactify.Definitions;

namespace Pactify.Utils
{
    internal static class PactifyUtils
    {
        public static string CreatePactFilePath(PactDefinition definition)
            => CreatePactFilePath(definition.Consumer.Name, definition.Provider.Name, definition.Options);

        public static string CreatePactFilePath(string consumer, string provider, PactDefinitionOptions options)
            => $"{options.DestinationPath}/{consumer}-{provider}.json";
    }
}

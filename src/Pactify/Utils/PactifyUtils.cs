using Pactify.Definitions;

namespace Pactify.Utils
{
    internal static class PactifyUtils
    {
        public static string CreatePactFilePath(PactDefinition definition, string localPath)
            => CreatePactFilePath(definition.Consumer.Name, definition.Provider.Name, localPath);

        public static string CreatePactFilePath(string consumer, string provider, string localPath)
            => $"{localPath}/{consumer}-{provider}.json";
    }
}

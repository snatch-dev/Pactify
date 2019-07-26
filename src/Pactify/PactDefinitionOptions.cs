namespace Pactify
{
    public class PactDefinitionOptions
    {
        public PublishType PublishType { get; set; }
        public string DestinationPath { get; set; }
        public bool IgnoreCasing { get; set; }
        public bool IgnoreContractValues { get; set; }
    }
}

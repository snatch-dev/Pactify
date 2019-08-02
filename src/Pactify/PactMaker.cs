using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Pactify.Builders;
using Pactify.Builders.Http;
using Pactify.Definitions;
using Pactify.Definitions.Http;
using Pactify.Messages;
using Pactify.Publishers;

namespace Pactify
{
    public class PactMaker : IPactMaker
    {
        private readonly PactDefinition _pactDefinition;
        private IPactPublisher _publisher;

        private PactMaker(PactDefinitionOptions options)
        {
            _pactDefinition = new PactDefinition
            {
                Options = options
            };
        }

        public static IPactMaker Create(PactDefinitionOptions options = null)
            => new PactMaker(options ?? new PactDefinitionOptions());

        public IPactMaker Between(string consumer, string provider)
        {
            if (string.IsNullOrEmpty(consumer) || string.IsNullOrEmpty(provider))
            {
                throw new PactifyException(ErrorMessages.ConsumerProviderMustBeDefined);
            }

            _pactDefinition.Consumer = new ConsumerDefinition { Name = consumer };
            _pactDefinition.Provider = new ProviderDefinition { Name = provider };
            return this;
        }

        public IPactMaker WithHttpInteraction(Action<IHttpInteractionBuilder> buildCoupling)
        {
            if (buildCoupling is null)
            {
                throw new PactifyException(ErrorMessages.InteractionMustBeDefined);
            }

            var builder = new HttpInteractionBuilder();
            buildCoupling(builder);

            var accessor = (IBuildingAccessor<HttpInteractionDefinition>)builder;
            var definition = accessor.Build();
            _pactDefinition.Interactions.Add(definition);

            return this;
        }

        public IPactMaker PublishedAsFile(string localPath)
        {
            _publisher = new FilePactPublisher(localPath);
            return this;
        }

        public IPactMaker PublishedViaHttp(string url, HttpMethod method, string apiKey = null)
        {
            _publisher = new HttpPactPublisher(url, method, apiKey);
            return this;
        }

        public void Make()
            => MakeAsync().GetAwaiter().GetResult();

        public async Task MakeAsync()
        {
            if (_publisher is null)
            {
                throw new PactifyException(ErrorMessages.PublisherNotSetUp);
            }
            await _publisher.PublishAsync(_pactDefinition);
        }
    }
}

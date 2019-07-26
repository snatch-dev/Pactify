using System;
using System.Linq;
using System.Threading.Tasks;
using Pactify.Retrievers;

namespace Pactify.Verifiers
{
    internal sealed class PactVerifier : IPactVerifier
    {
        private readonly PactDefinitionOptions _options;
        private readonly IPactRetriever _retriever;
        private readonly ICouplingVerifierDispatcher _dispatcher;

        public PactVerifier(PactDefinitionOptions options, IPactRetriever retriever,
            ICouplingVerifierDispatcher dispatcher)
        {
            _options = options;
            _retriever = retriever;
            _dispatcher = dispatcher;
        }

        public async Task VerifyAsync()
        {
            var definition = _retriever.Retrieve(_options);

            var resultTasks = definition.Couplings
                .Select(c => _dispatcher.DispatchAsync(c, _options))
                .ToList();

            await Task.WhenAll(resultTasks);

            var result = resultTasks
                .Select(t => t.Result)
                .Aggregate((c, next) => c & next);

            if (result.IsSuccessful)
            {
                return;
            }

            throw new PactifyException(string.Join(Environment.NewLine, result.Errors));
        }

        public void Verify()
            => VerifyAsync().GetAwaiter().GetResult();
    }
}

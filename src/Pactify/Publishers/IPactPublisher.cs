using System.Threading.Tasks;
using Pactify.Definitions;

namespace Pactify.Publishers
{
    internal interface IPactPublisher
    {
        Task PublishAsync(PactDefinition definition);
    }
}

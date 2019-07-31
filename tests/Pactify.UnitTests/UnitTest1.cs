using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Pactify.Serialization;
using Xunit;

namespace Pactify.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Consumer_Should_Create_APact()
        {
            var options = new PactDefinitionOptions
            {
                IgnoreContractValues = true,
                IgnoreCasing = true
            };

            PactMaker
                .Create(options)
                .Between("orders", "parcels")
                .WithHttpCoupling(cb => cb
                    .Given("There is a parcel with some id")
                    .UponReceiving("A GET Request to retrieve the parcel")
                    .With( request => request
                        .WithMethod(HttpMethod.Get)
                        .WithPath("api/parcels/1"))
                    .WillRespondWith(response => response
                        .WithHeader("Content-Type", "application/json")
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBody<ParcelReadModel>()))
                .PublishedViaHttp("http://localhost:9292/pacts/provider/parcels/consumer/orders/version/1.2.104", HttpMethod.Put)
                .Make();
        }

        [Fact]
        public async Task Provider_Should_Meet_Consumers_Expectations()
        {
            await PactVerifier
                .CreateFor<Startup>()
                .Between("orders", "parcels")
                .RetrievedViaHttp("http://localhost:9292/pacts/provider/parcels/consumer/orders/latest")
                .VerifyAsync();
        }
    }
}

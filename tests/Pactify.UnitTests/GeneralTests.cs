using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Pactify.UnitTests
{
    public class GeneralTests
    {
        [Fact]
        public async Task Consumer_Should_Create_APact()
        {
            var options = new PactDefinitionOptions
            {
                IgnoreContractValues = true,
                IgnoreCasing = true
            };

            await PactMaker
                .Create(options)
                .Between("orders", "parcels")
                .WithHttpInteraction(cb => cb
                    .Given("There is a parcel with some id")
                    .UponReceiving("A GET Request to retrieve the parcel")
                    .With( request => request
                        .WithMethod(HttpMethod.Get)
                        .WithPath("api/parcels/{Id}"))
                    .WillRespondWith(response => response
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBody<ParcelReadModel>()))
                .PublishedViaHttp("http://localhost:9292/pacts/provider/parcels/consumer/orders/version/1.2.104", HttpMethod.Put)
                .MakeAsync();
        }

        [Fact]
        public async Task Consumer_Should_Create_APact_For_Lists()
        {
            var options = new PactDefinitionOptions
            {
                IgnoreContractValues = true,
                IgnoreCasing = true
            };

            await PactMaker
                .Create(options)
                .Between("orders", "parcels")
                .WithHttpInteraction(cb => cb
                    .Given("There is a list of parcels")
                    .UponReceiving("A GET Request to retrieve the parcels")
                    .With(request => request
                       .WithMethod(HttpMethod.Get)
                       .WithPath("api/parcels"))
                    .WillRespondWith(response => response
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBody<List<ParcelReadModel>>()))
                .PublishedAsFile("./Pacts")
                .MakeAsync();
        }

        [Fact]
        public async Task Provider_Should_Meet_Consumers_Expectations()
        {
            await PactVerifier
                .CreateFor<Startup>()
                .UseEndpointTemplate(new ParcelReadModel())
                .Between("orders", "parcels")
                .RetrievedViaHttp("http://localhost:9292/pacts/provider/parcels/consumer/orders/latest")
                .VerifyAsync();
        }

        [Fact]
        public async Task Provider_Should_Meet_Consumers_List_Expectations()
        {
            await PactVerifier
                .CreateFor<Startup>()
                .UseEndpointTemplate(new ParcelReadModel())
                .Between("orders", "parcels")
                .RetrievedFromFile("./Pacts")
                .VerifyAsync();
        }
    }
}

using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Pactify.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
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
                        .WithMethod("GET")
                        .WithPath("api/parcels/1"))
                    .WillRespondWith(response => response
                        .WithHeader("Content-Type", "application/json")
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBody<ParcelReadModel>()))
                .PublishedAsFile("../../../../../pacts")
                .Make();



            await PactVerifier
                .CreateFor<Startup>()
                .Between("orders", "parcels")
                .RetrievedFromFile("../../../../../pacts")
                .VerifyAsync();
        }
    }
}

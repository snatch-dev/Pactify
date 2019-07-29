using System;
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
                IgnoreContractValues = false,
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

//            var testServer = new  TestServer(new WebHostBuilder().UseStartup<Program>());
//            var client = testServer.CreateClient();

//            var verifier = PactVerifierBuilder
//                .Create(options)
//                .Between("orders", "parcels")
//                .UsingHttpClient(client)
//                .Build();
//
//            await verifier
//                .VerifyAsync();
        }
    }

    public class ParcelReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ParcelReadModel(Guid id)
        {
            Id = id;
        }
    }
}

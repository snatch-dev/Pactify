using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
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
                DestinationPath = "../../../../../pacts",
                IgnoreContractValues = false
            };

            PactBuilder
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
                        .WithBody(new ParcelReadModel {Id = new Guid("14dfc984-4cc1-4037-87f7-579ae2b8f0bc"), Name = "TV", Price = 21.37m})))
                .Make();

            var testServer = new  TestServer(new WebHostBuilder().UseStartup<Program>());
            var client = testServer.CreateClient();

            var verifier = PactVerifierBuilder
                .Create(options)
                .Between("orders", "parcels")
                .UsingHttp(client)
                .Build();

            await verifier
                .VerifyAsync();
        }
    }

    public class ParcelReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

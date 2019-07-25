using System;
using System.Net;
using Pactify.Definitions;
using Xunit;

namespace Pactify.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            PactBuilder
                .CreateForHttp(new PactDefinitionOptions {DestinationPath = "../../../../../pacts"})
                .Between("orders", "parcels")
                .WithCoupling(cb => cb
                    .Given("There is a parcel with some id")
                    .UponReceiving("A GET Request to retrieve the parcel")
                    .With( request => request
                        .WithMethod("GET")
                        .WithPath("api/parcels/1"))
                    .WillRespondWith(response => response
                        .WithHeader("Content-Type", "application/json")
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBody<ParcelReadModel>()))
                .Make();
        }
    }

    public class ParcelReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

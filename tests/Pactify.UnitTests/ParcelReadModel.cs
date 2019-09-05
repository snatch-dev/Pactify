using System;

namespace Pactify.UnitTests
{
    public class ParcelReadModel
    {
        public Guid Id { get; set; } = new Guid("9b5055dd-5750-4c65-ab4c-9a785a9b7ef4");

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

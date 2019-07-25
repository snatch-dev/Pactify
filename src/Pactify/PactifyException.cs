using System;

namespace Pactify
{
    public class PactifyException : Exception
    {
        public PactifyException(string message) : base(message) { }
    }
}

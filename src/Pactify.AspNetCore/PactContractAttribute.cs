using System;

namespace Pactify.AspNetCore
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PactContractAttribute : Attribute
    {
        public string[] Providers { get; }

        public PactContractAttribute(params string[] providers)
        {
            Providers = providers;
        }
    }
}

using System;
using System.Net;

namespace Pactify.Messages
{
    internal static class ErrorMessages
    {
        public static readonly string ConsumerProviderMustBeDefined = "Both consumer and provider must be defined";
        public static readonly string InteractionMustBeDefined = "Interaction must be defined";
        public static readonly string PublisherNotSetUp = "Publish type ahs not been set up";
        public static readonly string UnknownHttpMethodDefined = "Unknown HTTP method defined";
        public static readonly string IncorrectResponseStatusCode = "Expected response status code {0}, but was {1}";
        public static readonly string MissingResponseHeader = "Expected response header {0} was not present.";
        public static readonly string IncorrectReposnseHeaderValue = "Expected response header {0} to have value {1}, but was {2}";
        public static readonly string MissingResponseBodyProperty = "Expected response body property {0} was not present.";
        public static readonly string IncorrectReposnseBodyPropertyValue = "Expected response body property {0} to have value {1}, but was {2}";
    }
}

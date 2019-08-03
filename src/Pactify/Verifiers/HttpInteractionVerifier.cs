using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pactify.Definitions;
using Pactify.Definitions.Http;
using Pactify.Messages;

namespace Pactify.Verifiers
{
    internal sealed class HttpInteractionVerifier
    {
        private readonly HttpClient _httpClient;

        public HttpInteractionVerifier(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PactVerificationResult> VerifyAsync(HttpInteractionDefinition definition, PactDefinitionOptions options)
        {
            var getResult = GetHttpMethod(definition.Request.Method);
            var httpResponse = await getResult(definition.Request.Path);
            var json = await httpResponse.Content.ReadAsStringAsync();
            var providedBody = JsonConvert.DeserializeObject<ExpandoObject>(json);
            var expectedBody = definition.Response.Body;
            var errors = new List<string>();

            VerifyStatusCode(definition, httpResponse, errors);
            VerifyHeaders(definition, httpResponse, errors);

            if (providedBody is null || expectedBody is null)
            {
                return new PactVerificationResult(errors);
            }

            VerifyBody(options, expectedBody, providedBody, errors);
            return new PactVerificationResult(errors);
        }

        private static void VerifyStatusCode(HttpInteractionDefinition definition, HttpResponseMessage response, List<string> errors)
        {
            if(response.StatusCode != definition.Response.Status)
            {
                var message = GetErrorMessage(ErrorMessages.IncorrectResponseStatusCode,
                    definition.Response.Status, response.StatusCode);
                errors.Add(message);
            }
        }

        private static void VerifyHeaders(HttpInteractionDefinition definition, HttpResponseMessage response, List<string> errors)
        {
            foreach (var header in definition.Response.Headers)
            {
                var (name, value) = response.Headers
                    .Concat(response.Content.Headers)
                    .Where(h => h.Key == header.Key)
                    .Select(h => (Name: h.Key, Value: h.Value.FirstOrDefault()))
                    .FirstOrDefault();

                if (name is null)
                {
                    var message = GetErrorMessage(ErrorMessages.MissingResponseHeader, header.Key);
                    errors.Add(message);
                }
                else if (value != header.Value)
                {
                    var message = GetErrorMessage(ErrorMessages.IncorrectReposnseHeaderValue, header.Key, value);
                    errors.Add(message);
                }
            }
        }

        private static string GetErrorMessage(string message, params object[] messageParams)
            => string.Format(message, messageParams);

        private static void VerifyBody(PactDefinitionOptions options, object expectedBody,
            IDictionary<string, object> providedBody, List<string> errors)
        {
            foreach (var pair in (IDictionary<string, object>)expectedBody)
            {
                var stringComparision = options.IgnoreCasing
                    ? StringComparison.InvariantCultureIgnoreCase
                    : StringComparison.InvariantCulture;
                var propertyName = pair.Key;
                var propertyValue = pair.Value;

                var providedProperty = providedBody.FirstOrDefault(p => p.Key.Equals(propertyName, stringComparision)).Value;

                if (providedProperty is null)
                {
                    var message = GetErrorMessage(ErrorMessages.MissingResponseBodyProperty, propertyName);
                    errors.Add(message);
                }
                else
                {
                    if (options.IgnoreContractValues)
                    {
                        continue;
                    }

                    var propertyHasExpectedValue = providedProperty.Equals(propertyValue);

                    if (!propertyHasExpectedValue)
                    {
                        var message = GetErrorMessage(ErrorMessages.IncorrectReposnseBodyPropertyValue, propertyValue,
                            providedProperty);
                        errors.Add(message);
                    }
                }
            }
        }

        private Func<string, Task<HttpResponseMessage>> GetHttpMethod(string method)
        {
            switch (method)
            {
                case "GET":
                    return path => _httpClient.GetAsync(path);
                case "POST":
                    return path => _httpClient.PostAsync(path, null);
                case "PUT":
                    return path => _httpClient.PutAsync(path, null);
                case "DELETE":
                    return path => _httpClient.DeleteAsync(path);
                default:
                    throw new PactifyException(ErrorMessages.UnknownHttpMethodDefined);
            }
        }
    }
}

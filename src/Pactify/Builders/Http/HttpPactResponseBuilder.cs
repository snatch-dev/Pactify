using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using Pactify.Definitions.Http;

namespace Pactify.Builders.Http
{
    internal sealed class HttpPactResponseBuilder : IHttpPactResponseBuilder, IBuildingAccessor<HttpPactResponse>
    {
        private readonly HttpPactResponse _pactResponse;

        public HttpPactResponseBuilder()
        {
            _pactResponse = new HttpPactResponse();
        }

        public IHttpPactResponseBuilder WithHeader(string key, string value)
        {
            _pactResponse.Headers.Add(key, value);
            return this;
        }

        public IHttpPactResponseBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            _pactResponse.Status = statusCode;
            return this;
        }

        public IHttpPactResponseBuilder WithBody(object body)
        {
            if (body is null)
            {
                throw new PactifyException("HTTP Body must be provided");
            }

            _pactResponse.Body = body;
            return this;
        }

        public IHttpPactResponseBuilder WithBody<TBody>()
        {
            var isListType = typeof(TBody).GetMethods().FirstOrDefault(x => x.Name.Equals("Add")) != null;

            if (isListType)
            {
                var listItemType = typeof(TBody).GetGenericArguments().Single();
                var list = new List<object>
                {
                    Activator.CreateInstance(listItemType)
                };

                return WithBody(list);
            }

            return WithBody(FormatterServices.GetSafeUninitializedObject(typeof(TBody)));
        }

        public HttpPactResponse Build()
            => _pactResponse;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Pactify.AspNetCore
{
    public static class Extensions
    {
        public static IServiceCollection AddPactify(this IServiceCollection services)
        {
            services.AddRouting();
            return services;
        }

        public static IApplicationBuilder UsePactifyContractsEndpoint(this IApplicationBuilder app)
        {
            var contracts = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsDefined(typeof(PactContractAttribute), false))
                .Select(t => new {Providers = t.GetCustomAttribute<PactContractAttribute>().Providers, Type = t});

            app.UseRouter(builder =>
                builder.MapGet("/pacts/{provider}", async (request, response, data) =>
                {
                    var provider = (string) request.HttpContext.GetRouteData().Values["provider"];

                    var responseBody = contracts
                        .Where(a => provider is null || a.Providers.Any(p => p.Equals(provider, StringComparison.InvariantCultureIgnoreCase)))
                        .Select(a => a.Type)
                        .Select(t => new KeyValuePair<string, object>(t.Name, FormatterServices.GetUninitializedObject(t)))
                        .ToDictionary(kv => kv.Key, kv => kv.Value);

                    var json = JsonConvert.SerializeObject(responseBody);

                    response.StatusCode = 200;
                    await response.WriteAsync(json);
                }));

            return app;
        }

    }
}

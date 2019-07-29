using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Pactify.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRouter(builder =>
            {
                builder.MapGet("/api/parcels/1", async (request, response, data) =>
                {
                    var parcel = new ParcelReadModel {Id = Guid.NewGuid(), Name = "Test", Price = 10};
                    var json = JsonConvert.SerializeObject(parcel);
                    await response.WriteAsync(json);
                });
            });
        }
    }
}

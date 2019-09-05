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

                var id = new Guid("9b5055dd-5750-4c65-ab4c-9a785a9b7ef4");
                builder.MapGet($"/api/parcels/{id}", async (request, response, data) =>
                {
                    var parcel = new ParcelReadModel {Id = id, Name = "Test", Price = 10};
                    var json = JsonConvert.SerializeObject(parcel);
                    await response.WriteAsync(json);
                });
            });
        }
    }
}

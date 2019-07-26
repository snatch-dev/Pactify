using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace TestWebApp
{
    public class ParcelReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRouter(builder =>
                builder.MapGet("api/parcels/1", async (request, response, data) =>
                {
                    response.StatusCode = 200;
                    var readModel = new ParcelReadModel {Id = new Guid("14dfc984-4cc1-4037-87f7-579ae2b8f0bc"), Name = "TV", Price = 21.37m};
                    var json = JsonConvert.SerializeObject(readModel);
                    await response.WriteAsync(json);
                }));
        }
    }
}

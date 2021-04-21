using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrjModule25_Parser.Controllers;
using PrjModule25_Parser.Models.Hubs;
using PrjModule25_Parser.Service;
using PrjModule25_Parser.Service.TimedHostedServices;

namespace PrjModule25_Parser
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Cors policy
            services.AddCors();

            //Data base connection
            var connectionString = Configuration.GetConnectionString("UserDb");
            services.AddDbContext<ApplicationDb>(options =>
                options.UseSqlServer(connectionString));

            //SignalR connection
            services.AddSignalR();

            //Make services from controllers
            services.AddScoped<ProductController>();
            services.AddScoped<ShopController>();

            //Background workers for parsing data
            services.AddHostedService<BackgroundProductControllerWorker>();
            services.AddHostedService<BackgroundShopControllerWorker>();

            services.AddControllers();
            services.AddOpenApiDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()// allow credentials
                .WithExposedHeaders("Content-Disposition")); 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ApiHub>("/hubs/DataFetchHub");
            });
        }
    }
}
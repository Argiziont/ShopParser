using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopParserApi.Models;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Services;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.Repositories;
using ShopParserApi.Services.Repositories.Interfaces;
using ShopParserApi.Services.TimedHostedServices.BackgroundWorkItems;

namespace ShopParserApi
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

            services.AddTransient<ICategoryRepository, CategoryRepository>(provider =>
                new CategoryRepository(connectionString));
            services.AddTransient<IProductRepository, ProductRepository>(provider =>
                new ProductRepository(connectionString));
            services.AddTransient<ICompanyRepository, CompanyRepository>(provider =>
                new CompanyRepository(connectionString));

            //SignalR connection
            services.AddSignalR();

            //Services scopes
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IBrowsingContextService, BrowsingContextService>();

            services.AddSingleton<IBackgroundTaskQueue<ProductData>>(ctx =>
            {
                if (!int.TryParse(Configuration["QueueCapacity"], out var queueCapacity))
                    queueCapacity = 100000;
                return new BackgroundProductsQueue(queueCapacity);
            });

            services.AddSingleton<IBackgroundTaskQueue<CompanyData>>(ctx =>
            {
                if (!int.TryParse(Configuration["QueueCapacity"], out var queueCapacity))
                    queueCapacity = 10;
                return new BackgroundCompaniesQueue(queueCapacity);
            });

            //Background workers for parsing data
            //services.AddHostedService<BackgroundProductControllerWorker>();
            //services.AddHostedService<BackgroundCompanyControllerWorker>();

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
                .AllowCredentials() // allow credentials
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
using AutoMapper;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Services;
using ShopParserApi.Services.GraphQlServices;
using ShopParserApi.Services.GraphQlServices.GeneratedService;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.MapperService;
using ShopParserApi.Services.Repositories;
using ShopParserApi.Services.Repositories.Interfaces;
using ShopParserApi.Services.TimedHostedServices.BackgroundWorkItems;
using CompanyData = ShopParserApi.Models.CompanyData;
using ProductData = ShopParserApi.Models.ProductData;

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

            //AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoryMappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            mapperConfig.AssertConfigurationIsValid();//Check if map profiles is valid

            //Dapper repository wrappers
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>(provider =>
                new ProductRepository(connectionString));
            services.AddTransient<ICompanyRepository, CompanyRepository>(provider =>
                new CompanyRepository(connectionString));
            services.AddTransient<IDapperExecutorFactory, DapperExecutorFactory>();

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

            services.AddHttpClient<CategoryClient>();
            services.AddHttpClient<ProductClient>();
            services.AddHttpClient<CompanyClient>();

            services
                .AddRouting()
                .AddGraphQLServer()
                .AddInMemorySubscriptions()
                .AddSubscriptionType<SubscriptionObjectType>()
                .AddQueryType(t => t.Name(nameof(QueryService)))
                .AddType<CategoryQueryType>()
                .AddType<CompanyQueryType>()
                .AddType<ProductQueryType>();

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

            app.UseWebSockets();
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
                endpoints.MapGraphQL().WithOptions(new GraphQLServerOptions
                {
                    AllowedGetOperations = AllowedGetOperations.QueryAndMutation
                });
            });
        }
    }
}
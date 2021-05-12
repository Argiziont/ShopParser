using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Models.Hubs.Clients;
using ShopParserApi.Services.Exceptions;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services.TimedHostedServices
{
    public class BackgroundProductControllerWorker : BackgroundService
    {
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IHubContext<ApiHub, IApiClient> _productsHub;
        private readonly IServiceProvider _serviceProvider;

        public BackgroundProductControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider, IHubContext<ApiHub, IApiClient> productsHub,
            IBackgroundTaskQueue<ProductData> taskQueue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _productsHub = productsHub;
            TaskQueue = taskQueue;
        }

        private IBackgroundTaskQueue<ProductData> TaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDb>();
            var productService = scope.ServiceProvider.GetService<IProductService>();

            await TaskQueue.QueueBackgroundWorkItemsRangeAsync(context?.Products
                .Where(p => p.ProductState == ProductState.Idle));

            await BackgroundProcessing(stoppingToken, productService, context);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken, IProductService productService,
            ApplicationDb context)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var dequeuedProduct = await TaskQueue.DequeueAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                try
                {
                    while (dequeuedProduct != null)
                    {
                        if (context.Products.Count(p => p.ProductState == ProductState.Idle) == 0)
                            continue;
                        if (context.Companies.Count(s =>
                            s.CompanyState == CompanyState.Processing || s.CompanyState == CompanyState.Idle) != 0)
                            continue;


                        if (dequeuedProduct == null)
                        {
                            _logger.LogError("Something went wrong with background product parser");
                            throw new NullReferenceException();
                        }

                        try
                        {
                            await productService.InsertProductPageIntoDb(dequeuedProduct);
                        }
                        catch (TooManyRequestsException)
                        {
                            dequeuedProduct.ProductState = ProductState.Failed;
                            _logger.LogError(
                                $"Product with id \"{dequeuedProduct.Id}\" couldn't be updated. Blocked by service provider.");
                        }

                        await _productsHub.Clients.All.ReceiveMessage(
                            $"Product with name id: {dequeuedProduct.ExternalId} was updated successfully");
                        _logger.LogInformation(
                            $"Product with name id: {dequeuedProduct.ExternalId} was updated successfully");

                        dequeuedProduct = null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred in BackgroundProductControllerWorker.");
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service stopped.");

            await base.StopAsync(stoppingToken);
        }
    }
}
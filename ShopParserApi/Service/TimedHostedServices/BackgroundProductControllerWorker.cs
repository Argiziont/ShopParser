using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Models.Hubs.Clients;
using ShopParserApi.Service.Exceptions;
using ShopParserApi.Service.Helpers;

namespace ShopParserApi.Service.TimedHostedServices
{
    public class BackgroundProductControllerWorker : IHostedService, IDisposable
    {
        private readonly IBrowsingContext _context;
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IHubContext<ApiHub, IApiClient> _productsHub;
        private readonly IServiceProvider _serviceProvider;

        public BackgroundProductControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider, IHubContext<ApiHub, IApiClient> productsHub)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _logger = logger;
            _serviceProvider = serviceProvider;
            _productsHub = productsHub;
        }

        public void Dispose()
        {
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service running.");

            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            await DoWork(ct);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service is stopping.");

            return Task.CompletedTask;
        }

        private async Task DoWork(CancellationToken ct)
        {
            await Task.Run((async () =>
            {
                while (!ct.IsCancellationRequested)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetService<ApplicationDb>();
                    try
                    {
                        if (context == null) return;

                        if (context.Products.Count(p => p.ProductState == ProductState.Idle) == 0)
                            return;
                        if (context.Shops.FirstOrDefault(s => s.Products.Count == 0) != null)
                            return;

                        //first product with idle state

                        var product = context.Products.FirstOrDefault(p => p.ProductState == ProductState.Idle);
                        if (product == null)
                        {
                            _logger.LogError("Something went wrong with background product parser");
                            throw new NullReferenceException();
                        }

                        try
                        {
                            var productPage = await _context.OpenAsync(product.Url);
                            if (productPage.StatusCode == HttpStatusCode.TooManyRequests)
                                throw new TooManyRequestsException();

                            await ProductService.ParseSinglePageAndInsertToDb(productPage, product.Url, context);
                        }
                        catch (TooManyRequestsException)
                        {
                            product.ProductState = ProductState.Failed;
                            _logger.LogError($"Product with id \"{product.Id}\" couldn't be updated");
                        }

                        await _productsHub.Clients.All.ReceiveMessage(
                            $"Product with name id: {product.ExternalId} was updated successfully");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                    await Task.Delay(TimeSpan.FromSeconds(5), ct);
                }

            }), ct);
        }
    }
}
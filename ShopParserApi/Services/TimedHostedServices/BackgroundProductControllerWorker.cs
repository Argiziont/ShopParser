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
using ShopParserApi.Services.Exceptions;

namespace ShopParserApi.Services.TimedHostedServices
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
            var config = Configuration.Default.WithDefaultLoader().WithJs().WithCss();
            _context = BrowsingContext.New(config);
            _logger = logger;
            _serviceProvider = serviceProvider;
            _productsHub = productsHub;
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service running.");

            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            Task.Run(() => DoWork(ct), ct);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service is stopping.");

            return Task.CompletedTask;
        }

        private async Task DoWork(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), ct);
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetService<ApplicationDb>();
                var productService = scope.ServiceProvider.GetService<ProductService>();
                try
                {
                    if (context == null) throw new NullReferenceException(nameof(context));
                    if (productService == null) throw new NullReferenceException(nameof(productService));

                    if (context.Products.Count(p => p.ProductState == ProductState.Idle) == 0)
                        continue;
                    if (context.Companies.Count(s =>
                        s.CompanyState == CompanyState.Processing && s.CompanyState == CompanyState.Idle) != 0)
                        continue;

                    //first product with idle state

                    var product = context.Products.FirstOrDefault(p => p.ProductState == ProductState.Idle);
                    if (product == null)
                    {
                        _logger.LogError("Something went wrong with background product parser");
                        throw new NullReferenceException();
                    }

                    try
                    {
                        //var productPage = await _context.OpenAsync(product.Url, ct);

                        //if (productPage.StatusCode == HttpStatusCode.TooManyRequests)
                        //    throw new TooManyRequestsException();

                        await productService.InsertPageIntoDb(product);
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
            }
        }
    }
}
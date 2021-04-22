using AngleSharp;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Models.Hubs.Clients;
using ShopParserApi.Service.Helpers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopParserApi.Service.TimedHostedServices
{
    public class BackgroundShopControllerWorker : IHostedService, IDisposable
    {
        private readonly IBrowsingContext _context;
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<ApiHub, IApiClient> _shopHub;

        public BackgroundShopControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider, IHubContext<ApiHub, IApiClient> shopHub)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _logger = logger;
            _serviceProvider = serviceProvider;
            _shopHub = shopHub;
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
                try
                {
                    if (context == null) throw new NullReferenceException(nameof(context));

                    var shop = context.Shops.FirstOrDefault(p => p.Products.Count == 0);
                    if (shop == null) continue;

                    var sellerPage = await _context.OpenAsync(shop.Url, ct);

                    await ShopService.AddProductsFromSellerPageToDb(shop, sellerPage, context, _shopHub);


                    context.Entry(shop).State = EntityState.Modified;

                    await context.SaveChangesAsync(ct);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }
        }
    }
}
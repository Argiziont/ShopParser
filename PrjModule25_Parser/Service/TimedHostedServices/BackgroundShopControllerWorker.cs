using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrjModule25_Parser.Controllers;
using PrjModule25_Parser.Models.Helpers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Service.TimedHostedServices
{
    public class BackgroundShopControllerWorker : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ShopController _shopController;
        private Timer _timer;

        public BackgroundShopControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _shopController = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ShopController>();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDb>();
            try
            {
                if (context == null) return;
                if (context.Products.Count(p => p.ProductState == ProductState.Idle) != 0) return;

                //first product with idle
                var shop = context.Shops.FirstOrDefault(p => p.Products.Count == 0);
                if (shop == null) return;
                var parsedUrls = _shopController.AddProductsListFromSellerAsync(shop.Name).Result;

                //_logger.LogInformation($"Urls in shop with id {shop.Id} successfully parsed");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            
        }
    }
}
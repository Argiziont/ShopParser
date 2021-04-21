using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrjModule25_Parser.Controllers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PrjModule25_Parser.Models.Hubs;
using PrjModule25_Parser.Models.Hubs.Clients;
using PrjModule25_Parser.Service.Helpers;

namespace PrjModule25_Parser.Service.TimedHostedServices
{
    public class BackgroundShopControllerWorker : IHostedService, IDisposable
    {
        private readonly IBrowsingContext _context;
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<ApiHub, IApiClient> _shopHub;
        private Timer _timer;

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

        private async  void DoWork(object state)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDb>();
            try
            {
                var shop = context?.Shops.FirstOrDefault(p => p.Products.Count == 0);
                if (shop == null) return;
                    
                var sellerPage = await _context.OpenAsync(shop.Url);

                await ShopService.AddProductsFromSellerPageToDb(shop, sellerPage, context, _shopHub);


                context.Entry(shop).State = EntityState.Modified;

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrjModule25_Parser.Controllers;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.Helpers;

namespace PrjModule25_Parser.Service.TimedHostedServices
{
    public class BackgroundProductControllerWorker : IHostedService, IDisposable
    {
        private readonly object _lockerObject = new();
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly ProductController _productController;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public BackgroundProductControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            //_dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDb>();
            _productController = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ProductController>();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(7));

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
            lock (_lockerObject)
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetService<ApplicationDb>();
                try
                {
                    if (context != null &&
                        context.Products.Count(p => p.ProductState == ProductState.Idle) == 0) return;
                    //first product with idle
                    if (context == null) return;
                    {
                        var product = context.Products.FirstOrDefault(p => p.ProductState == ProductState.Idle);
                        var parsedProduct = _productController
                            .ParseSingleProductInsideSellerPageAsync(product?.Id.ToString()).Result;
                        switch (parsedProduct)
                        {
                            case OkObjectResult okObject:
                            {
                                var okProduct = (ProductData) okObject.Value;
                                //_logger.LogInformation($"Successfully parsed product with id \"{okProduct.Id}\"");
                                return;
                            }
                            case BadRequestObjectResult badRequestObject:
                            {
                                var badRequestProduct = (string) badRequestObject.Value;
                                _logger.LogError($"Parse returned error with text \"{badRequestProduct}\"");
                                return;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }
            }
        }
    }
}
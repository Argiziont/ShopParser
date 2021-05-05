using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services.TimedHostedServices
{
    public class BackgroundCompanyControllerWorker : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BackgroundCompanyControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
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
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDb>();
            var companyService = scope.ServiceProvider.GetService<ICompanyService>();
            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), ct);

                
                try
                {
                    if (context == null) throw new NullReferenceException(nameof(context));
                    if (companyService == null) throw new NullReferenceException(nameof(companyService));

                    var company = context.Companies.FirstOrDefault(p => p.Products.Count == 0);
                    if (company == null) continue;
                    
                    await companyService.InsertCompanyIntoDb(company);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }
        }
    }
}
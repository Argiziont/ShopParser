using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopParserApi.Services.Helpers;

namespace ShopParserApi.Services.TimedHostedServices
{
    public class BackgroundCompanyControllerWorker : IHostedService, IDisposable
    {
        private readonly IBrowsingContext _context;
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BackgroundCompanyControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider)
        {
            var config = Configuration.Default.WithDefaultLoader().WithJs();
            _context = BrowsingContext.New(config);
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
            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), ct);

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetService<ApplicationDb>();
                try
                {
                    if (context == null) throw new NullReferenceException(nameof(context));

                    var company = context.Companies.FirstOrDefault(p => p.Products.Count == 0);
                    if (company == null) continue;

                    var companyPage = await _context.OpenAsync(company.Url, ct);

                    await CompanyService.AddProductsFromCompanyPageToDb(company, companyPage, context);


                    context.Entry(company).State = EntityState.Modified;

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
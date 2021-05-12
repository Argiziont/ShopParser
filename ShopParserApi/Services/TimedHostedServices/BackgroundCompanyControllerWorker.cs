using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopParserApi.Models;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services.TimedHostedServices
{
    public class BackgroundCompanyControllerWorker : BackgroundService
    {
        private readonly ILogger<BackgroundProductControllerWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IBackgroundTaskQueue<CompanyData> TaskQueue { get; }
        public BackgroundCompanyControllerWorker(ILogger<BackgroundProductControllerWorker> logger,
            IServiceProvider serviceProvider, IBackgroundTaskQueue<CompanyData> taskQueue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            TaskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Company Hosted Service running.");

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDb>();
            var companyService = scope.ServiceProvider.GetService<ICompanyService>();

            await TaskQueue.QueueBackgroundWorkItemsRangeAsync(context?.Companies
                .Where(c => c.Products.Count == 0));

            await BackgroundProcessing(stoppingToken,companyService);
        }
        private async Task BackgroundProcessing(CancellationToken stoppingToken, ICompanyService companyService)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var dequeuedCompany = await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);


                    var result = await companyService.InsertCompanyIntoDb(dequeuedCompany);
                    _logger.LogInformation($"Company with name {result.Name} was updated successfully");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        $"Error occurred in BackgroundProductControllerWorker.");
                }
            }
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Company Hosted Service stopped.");

            await base.StopAsync(stoppingToken);
        }
    }
}
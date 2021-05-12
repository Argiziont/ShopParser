using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using ShopParserApi.Models;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services.TimedHostedServices.BackgroundWorkItems
{
    public class BackgroundCompaniesQueue : IBackgroundTaskQueue<CompanyData>
    {
        private readonly Channel<CompanyData> _queue;

        public BackgroundCompaniesQueue(int capacity)
        {
            // Capacity should be set based on the expected application load and
            // number of concurrent threads accessing the queue.            
            // BoundedChannelFullMode.Wait will cause calls to WriteAsync() to return a task,
            // which completes only when space became available. This leads to backpressure,
            // in case too many publishers/calls start accumulating.
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<CompanyData>(options);
        }

        public async ValueTask QueueBackgroundWorkItemAsync(
            CompanyData company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            await _queue.Writer.WriteAsync(company);
        }

        public async ValueTask QueueBackgroundWorkItemsRangeAsync(IEnumerable<CompanyData> companies)
        {
            if (companies == null)
                throw new ArgumentNullException(nameof(companies));

            foreach (var company in companies) await _queue.Writer.WriteAsync(company);
        }

        public async ValueTask<CompanyData> DequeueAsync(
            CancellationToken cancellationToken)
        {
            var workItem = await _queue.Reader.ReadAsync(cancellationToken);

            return workItem;
        }
    }
}
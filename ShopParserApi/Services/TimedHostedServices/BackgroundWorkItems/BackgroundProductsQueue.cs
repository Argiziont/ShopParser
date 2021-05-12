using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using ShopParserApi.Models;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services.TimedHostedServices.BackgroundWorkItems
{
    public class BackgroundProductsQueue : IBackgroundTaskQueue<ProductData>
    {
        private readonly Channel<ProductData> _queue;

        public BackgroundProductsQueue(int capacity)
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
            _queue = Channel.CreateBounded<ProductData>(options);
        }

        public async ValueTask QueueBackgroundWorkItemAsync(
            ProductData product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            await _queue.Writer.WriteAsync(product);
        }

        public async ValueTask QueueBackgroundWorkItemsRangeAsync(IEnumerable<ProductData> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            foreach (var product in products) await _queue.Writer.WriteAsync(product);
        }

        public async ValueTask<ProductData> DequeueAsync(
            CancellationToken cancellationToken)
        {
            var workItem = await _queue.Reader.ReadAsync(cancellationToken);

            return workItem;
        }
    }
}
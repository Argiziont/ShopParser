﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShopParserApi.Services.Interfaces
{
    public interface IBackgroundTaskQueue<TItem>
    {
        ValueTask QueueBackgroundWorkItemAsync(TItem product);
        ValueTask QueueBackgroundWorkItemsRangeAsync(IEnumerable<TItem> products);

        ValueTask<TItem> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
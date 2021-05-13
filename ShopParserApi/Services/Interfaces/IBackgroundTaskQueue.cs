using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShopParserApi.Services.Interfaces
{
    public interface IBackgroundTaskQueue<TItem>
    {
        Task QueueBackgroundWorkItemAsync(TItem product);
        Task QueueBackgroundWorkItemsRangeAsync(IEnumerable<TItem> products);

        Task<TItem> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
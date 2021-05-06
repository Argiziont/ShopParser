using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ShopParserApi.Models.Hubs.Clients
{
    public interface IApiClient<T>
    {
        Task ReceiveMessage(string message);

        T All { get; }
}
    public interface IApiClient: IApiClient<IClientProxy>
    {
    }
}
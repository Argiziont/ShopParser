using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ShopParserApi.Models.Hubs.Clients
{
    public interface IApiClient
    {
        Task ReceiveMessage(string message);
        
    }
}
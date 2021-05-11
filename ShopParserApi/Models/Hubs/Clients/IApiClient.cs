using System.Threading.Tasks;

namespace ShopParserApi.Models.Hubs.Clients
{
    public interface IApiClient
    {
        Task ReceiveMessage(string message);
    }
}
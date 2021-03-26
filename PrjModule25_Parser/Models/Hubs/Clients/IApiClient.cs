using System.Threading.Tasks;

namespace PrjModule25_Parser.Models.Hubs.Clients
{
    public interface IApiClient
    {
        Task ReceiveMessage(string message);
    }
}
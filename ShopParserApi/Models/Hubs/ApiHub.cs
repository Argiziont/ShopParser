using Microsoft.AspNetCore.SignalR;
using ShopParserApi.Models.Hubs.Clients;

namespace ShopParserApi.Models.Hubs
{
    public class ApiHub : Hub<IApiClient>
    {
    }
}
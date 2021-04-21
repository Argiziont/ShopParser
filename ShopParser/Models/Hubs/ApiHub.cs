using Microsoft.AspNetCore.SignalR;
using PrjModule25_Parser.Models.Hubs.Clients;

namespace PrjModule25_Parser.Models.Hubs
{
    public class ApiHub : Hub<IApiClient>
    {
    }
}
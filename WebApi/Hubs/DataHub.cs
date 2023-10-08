using Microsoft.AspNetCore.SignalR;


namespace WebApi.Hubs
{
    public sealed class DataHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}



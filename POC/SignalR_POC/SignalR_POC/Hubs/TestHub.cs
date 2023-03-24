using Microsoft.AspNetCore.SignalR;
using SignalR_POC.Services;

namespace SignalR_POC.Hubs
{
    public class TestHub : Hub
    {
        public TestHub(GameService gameService)
        {
            this.gameService = gameService;
        }

        private readonly GameService gameService;

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            gameService.connections.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
            await DirectMessage("test");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("SendMessageToUser")]
        public async Task DirectMessage(string message)
        => await Clients.Client(gameService.connections[0]).SendAsync("SendMessageToUser", gameService.connections[0]);

        [HubMethodName("BroadcastMessage")]
        public async Task BroadcastMessage(string user, string message)
        => await Clients.All.SendAsync("BroadcastMessage", user, message);
    }
}

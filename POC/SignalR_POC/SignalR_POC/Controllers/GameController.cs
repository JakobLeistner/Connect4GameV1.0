using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_POC.Hubs;
using SignalR_POC.Services;

namespace SignalR_POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly TestHub testHub;
        private readonly IHubContext<TestHub> hubcontext;
        private readonly GameService gameService;

        public GameController(IHubContext<TestHub> hubcontext, GameService gameService)
        {
            this.hubcontext = hubcontext;
            this.gameService = gameService;
        }

        [HttpPost(Name = "Start")]
        public async Task Get()
        {
            await DirectMessage();
        }
        [HttpGet("Test")]
        public string Test()
        {
            return "Hello World";
        }

        private async Task DirectMessage()
          => await hubcontext.Clients.Client(gameService.connections[0]).SendAsync("SendMessageToUser", gameService.connections[0]);  
        //=> await hubcontext.Clients.User(gameService.user[0]).SendAsync("SendMessageToUser", gameService.user[0]);
    }
}
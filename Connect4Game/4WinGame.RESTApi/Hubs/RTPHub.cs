using _4WinGame.BusinessLogic.Contracts.Interfaces;
using _4WinGame.RESTApi.Contracts.Models;
using _4WinGame.RESTApi.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi.Hubs
{
    public class RTPHub : Hub
    {

        private readonly ConnectionService connectionService;
        private readonly IFourWinGamesService fourWinGamesService;

        public RTPHub(ConnectionService connectionService, IFourWinGamesService fourWinGamesService)
        {
            this.connectionService = connectionService;
            this.fourWinGamesService = fourWinGamesService;
        }

        public override async Task OnConnectedAsync()
        {
            connectionService.ConnectedIDs.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string foundID=connectionService.PlayerIDToConnectionIDlist.Where(k=>k.Value==Context.ConnectionId).FirstOrDefault().Key;
            if(foundID!=null)
            {
                IFourWinGame fourWinGame = fourWinGamesService.Games.Where(g => g.Player1.ID == foundID || g.Player2.ID == foundID).FirstOrDefault();
                BusinessLogic.Contracts.Models.FourWinGamePlayer leftPlayer = fourWinGamesService.AllPlayers.Where(p=>p.ID==foundID).FirstOrDefault();
                if(leftPlayer != null && fourWinGame != null)
                {
                    fourWinGamesService.LeaveActiveGame(leftPlayer, fourWinGame.ID);
                }
            }
            connectionService.ConnectedIDs.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception); 
            // prüfen, ob Connection ID gerade im Spiel
            // wenn ja, dann leave Game
        }

        [HubMethodName("GameStart")]
        public async Task GameStart(string gameID)
        {
            await SendMessage(gameID, "GameStart", gameID);
        }

        [HubMethodName("GameUpdated")]
        public async Task GameUpdated(string gameID)
        {
            await SendMessage(gameID, "GameUpdated", gameID);
        }

        [HubMethodName("WaitingListUpdated")]
        public async Task WaitingListUpdated()
        {
            await BroadcastMessage("WaitingListUpdated");
        }

        [HubMethodName ("GameFinished")]
        public async Task GameFinished(Player winner, string gameID)
        {
            await SendMessage(gameID, "GameFinished", winner);
        }

        private async Task BroadcastMessage(string message)
        {
           await Clients.All.SendAsync(message);
        }

        private async Task SendMessage(string gameID, string message, object data)
        {
            IFourWinGame targetGame = fourWinGamesService.GetGameByID(gameID);
            string connectionIDPlayer1;
            string connectionIDPlayer2;
            connectionService.PlayerIDToConnectionIDlist.TryGetValue(targetGame.Player1.ID, out connectionIDPlayer1);
            connectionService.PlayerIDToConnectionIDlist.TryGetValue(targetGame.Player2.ID, out connectionIDPlayer2);

            if (connectionIDPlayer1 != null)
            {
                await Clients.Client(connectionIDPlayer1).SendAsync(message, data);
            }
            if (connectionIDPlayer2 != null)
            {
                await Clients.Client(connectionIDPlayer2).SendAsync(message, data);
            }
        }

    }
}

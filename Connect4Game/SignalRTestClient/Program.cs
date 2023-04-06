using _4WinGame.RESTApi.Contracts.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRTestClient
{
    public class Program
    {
        static async Task Main(string[] args) {
            await TestSignalRClient();
        }

        private static async Task TestSignalRClient()
        {
            HubConnection hubConnection;
            string serverAddress = "https://localhost:44320/fourwingamehub";
            do
            {
                await Task.Delay(3000);
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(serverAddress)
                    .WithAutomaticReconnect()
                    .Build();
                Console.WriteLine("try to connect to hub (in TestSignalRClient)");
                try
                {
                    await hubConnection.StartAsync();
                } catch(Exception exception)
                {
                    Console.WriteLine($"Exception {exception}");
                }
            } while (hubConnection.State != HubConnectionState.Connected);

            Console.WriteLine("Connection to hub was successful");
            Console.WriteLine($"Connection-ID: {hubConnection.ConnectionId}");

            hubConnection.On<string>("GameUpdated", (gameid) => {
                Console.WriteLine("Game updated: ");
                Console.WriteLine(gameid); 
            });
            hubConnection.On<string>("GameStart", (gameid) => {
                Console.WriteLine("Game started: ");
                Console.WriteLine(gameid);
            });
            hubConnection.On<Player>("GameFinished", (winner) => {
                Console.WriteLine("Game finished with winner: ");
                Console.WriteLine(winner.PlayerName);
            });
            hubConnection.On("WaitingListUpdated",() => Console.WriteLine("List updated"));

            Console.ReadLine();
        }

    }
}

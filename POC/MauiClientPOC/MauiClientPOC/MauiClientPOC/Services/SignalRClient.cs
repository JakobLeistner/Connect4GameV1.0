using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiClientPOC.Services
{
    public class SignalRClient
    {
        HubConnection _hubConnection;
        public async Task StartConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:53353/Hub")
                .Build();
            //Subscribe To Event
            _hubConnection.On<string>("EventName", GetEvent);
            //Start Connection
            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void GetEvent(string message)
        {
            Console.WriteLine(message);
        }
    }
}

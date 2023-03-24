// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Hello, World!");
var _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44362/fourwingamehub", (opts) =>
                {
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)

                            // always verify the SSL certificate in Debug
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
                .WithAutomaticReconnect()
                .Build();
Action<string> actionOnMessage = (msg) =>
{
    Console.WriteLine(msg);
};
_hubConnection.On("SendMessageToUser", actionOnMessage);
try
{
    await _hubConnection.StartAsync();
    Console.WriteLine(_hubConnection.ConnectionID);
}
catch (Exception ex)
{

    throw;
}
do
{
    await Task.Delay(200);
} while (_hubConnection.State == HubConnectionState.Connecting);
Console.WriteLine(_hubConnection.State.ToString());


Console.ReadLine();



using Microsoft.AspNetCore.SignalR;

namespace Common_SignalR_ChatApp_dotnet.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Message(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            //this will be listen by client using javascript.
        }
    }
}

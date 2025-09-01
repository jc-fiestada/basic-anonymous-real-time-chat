using Microsoft.AspNetCore.SignalR;
using MiniChat.Model;
using MiniChat.DBServices;

namespace MiniChat.RealTimeConnection
{
    class Connection : Hub
    {
        public static HashSet<string> UserId = new HashSet<string>();
        public async override Task OnConnectedAsync()
        {
            UserId.Add(Context.ConnectionId);
            await Clients.All.SendAsync("/user-count-changed", UserId.Count);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            UserId.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("/user-count-changed", UserId.Count());
            await base.OnDisconnectedAsync(exception);
        }

        public async void SendMessage(string message, string user)
        {
            MessageDb database = new MessageDb();
            DateTime date = DateTime.Today;

            database.InsertMessage(message, user, date);

            MessageRequestDto data = new MessageRequestDto(message, user, date);
            
            await Clients.All.SendAsync("message-received", data);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Models
{
    [Authorize]
    public class ChatHub : Hub
    {
        static List<string> users = new List<string>();

        public async Task Send(string message, string id)
        {
            await Clients.All.SendAsync("Receive", Context.User.Identity.Name, message, id);
        }

        public async Task SendPrivate(string message, string toUser, string id)
        {
            string sender = Context.User.Identity.Name;
            await Clients.User(toUser).SendAsync("Receive", $"{sender}", message, id);
            await Clients.Caller.SendAsync("Receive", $"You -> {toUser}", message, id);
        }

        public async Task Delete(string id)
        {
            await Clients.All.SendAsync("DeleteMessage", id);
        }

        public override async Task OnConnectedAsync()
        {
            string userName = Context.User.Identity.Name;
            if (!users.Contains(userName))
            {
                users.Add(userName);
            }

            await Clients.All.SendAsync("UpdateUsers", users);
            await Clients.All.SendAsync("Notify", $"{userName} in chat");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userName = Context.User.Identity.Name;
            users.Remove(userName);

            await Clients.All.SendAsync("UpdateUsers", users);
            await Clients.All.SendAsync("Notify", $"{userName} left the chat");
            await base.OnDisconnectedAsync(exception);
        }
    }
}

using API.Services;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ContactsHub : Hub
    {
        public async void Connect(string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
        }
         
        public async Task ContactChanged(int refresh, string username)
        {
            try
            {
                await Clients.Group(username).SendAsync("ChangeRecived", refresh + 1);
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}

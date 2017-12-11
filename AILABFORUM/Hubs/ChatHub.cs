using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AILABFORUM.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string date, string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(date, name, message);
        }
    }
}
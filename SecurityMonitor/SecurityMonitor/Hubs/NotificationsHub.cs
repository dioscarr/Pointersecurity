using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SecurityMonitor.Controllers
{
    public class NotificationsHub : Hub
    {
        public void SendNotification(string author, string message)
        {
            Clients.All.broadcastNotification(author, message);
        }
    }
}


using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PointerSecurityDataLayer;
using SecurityMonitor.Workes;

namespace SecurityMonitor.Controllers
{
    public class NotificationsHub : Hub
    {

        public void SendNotification(string author, string message)
        {
            PointerSecurityEntities db = new PointerSecurityEntities();
            SignalRMessageTable OBJM = new SignalRMessageTable()
            {
                 Name=author,
                  message = message
            };

          
//tdfdfdfdfdfdfdfgfgfgfgdfdfdfgfgfgfgfgfgf



            Gmail gmail = new Gmail("pointerwebapp", "Dmc10040!");
            MailMessage msg = new MailMessage("pointerwebapp@gmail.com", "x.tina.molina89@gmail.com");
            msg.Subject = "Delivery Notification";
            msg.Body = message;
            gmail.Send(msg);




//gfgfgfgfgfgfgfgfgfgfgfhfghngfhdgfghfghfghf


            db.SignalRMessageTable.Add(OBJM);
            db.SaveChanges();
            Clients.All.broadcastNotification(author, message);
        }


        public void sendEmailNotification()
        {

           
        }
    }

    
}


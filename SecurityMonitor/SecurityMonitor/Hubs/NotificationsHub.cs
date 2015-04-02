using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Doormandondemand;
using SecurityMonitor.Workes;

namespace SecurityMonitor.Controllers
{
    public class NotificationsHub : Hub
    {
       

        public void SendNotification(string author, string message)
        {
            PointerdbEntities db = new PointerdbEntities();
            SignalRMessageTable OBJM = new SignalRMessageTable()
            {
                 Name=author,
                  message = message
            };

            string password = PasswordGenerator.GeneratePassword("8").ToString(); 
            string string1 = "Hi Firstname and lastname, An Account has been created for you by PointerWebApp.com ";
            string string2 = "The login information for apartment: apartment#: is below";
            string string3 = "Username: EmailAddress";
            string string4 = "Temporary password: " + password;
            string string5 = "Click the on this link and follow the instructions to initiate your account. ";
            string string6 = "Company Description";
            string string7 = "Find more information...";

            string x = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", string1, string2, string3, string4, string5, string6, string7);

            Gmail gmail = new Gmail("pointerwebapp", "Dmc10040!");
            MailMessage msg = new MailMessage("pointerwebapp@gmail.com", "pointerwebapp@gmail.com");
            msg.Subject = "Pointer Security New Account Notification";
            msg.Body = x;
            gmail.Send(msg);


            db.SignalRMessageTable.Add(OBJM);
            db.SaveChanges();
            Clients.All.broadcastNotification(author, message);
        }


        public void PackageAddedNotification(string BuildingID, string ApartmentID)
        {
            Clients.All.incomingPackageNotification(BuildingID, ApartmentID);
        }
        


        public void sendEmailNotification()
        {

           
        }
    }

    
}


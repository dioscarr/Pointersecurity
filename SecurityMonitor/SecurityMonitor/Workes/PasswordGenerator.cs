using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Doormandondemand;
using Microsoft.AspNet.Identity;

namespace SecurityMonitor.Workes
{
    

    public class PasswordGenerator
    {


        PointerdbEntities db = new PointerdbEntities();
        /// <summary>
        /// This will generate a Temp Password
        /// </summary>
        /// <param name="passwordLength">Length of the password</param>
        /// <returns>Data type string password </returns>        
        public static string GeneratePassword(string passwordLength)
        {
            
            //This one tells you how many characters the string will contain.
            string PasswordLength = passwordLength;
            //This one, is empty for now - but will ultimately hold the finised randomly generated password
            string NewPassword = "";

            //This one tells you which characters are allowed in this new password
            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "~,!,@,#,$,%,^,&,*,+,?";

            //Then working with an array...

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);

            string IDString = "";
            string temp = "";

            //utilize the "random" class
            Random rand = new Random();

            //and lastly - loop through the generation process...
            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

                //For Testing purposes, I used a label on the front end to show me the generated password.
                //lblProduct.Text = IDString;
            }
            
            return NewPassword;
        }



        private static string ResetPwdSendNotification(string ID)
        {
            PointerdbEntities db1 = new PointerdbEntities();
            try
            {

                 var password = GeneratePassword("8").ToString();
                 
                 PasswordHasher passwordsher = new PasswordHasher();
                 var pwdhashed = passwordsher.HashPassword(password);
               
                Tenant ObjTenant = db1.Tenant.Find(ID);
                AspNetUsers ObjAspnet = db1.AspNetUsers.Where(c => c.Email == ObjTenant.Username).FirstOrDefault();
                ObjAspnet.PasswordHash = pwdhashed;
                db1.AspNetUsers.Attach(ObjAspnet);
                var Entry = db1.Entry(ObjAspnet);
                Entry.Property(c=>c.PasswordHash).IsModified = true;
                db1.SaveChanges();

            if (ObjTenant!= null)
            {
                var aptNumber = db1.Apartment.Find(ObjTenant.aptID).ApartmentNumber;


                string string1 = "Hi " + ObjTenant.FirstName + " " + ObjTenant.LastName + ", your password has been reset by PointerWebApp.com ";
                string string2 = "The login information for apartment: " + aptNumber + " is below";
                string string3 = "Username: " + ObjTenant.Username;
                string string4 = "Temporary password: " + password;
                string string5 = "Click the on this http://localhost:64083/Account/Manage link and follow the instructions to initiate your account ";
                string string6 = "Company Description";
                string string7 = "Find more information...";

                string x = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", string1, string2, string3, string4, string5, string6, string7);

                Gmail gmail = new Gmail("pointerwebapp", "Dmc10040!");
                MailMessage msg = new MailMessage("pointerwebapp@gmail.com", ObjTenant.Username);
                msg.Subject = "Pointer Security Password Reset Notification";
                msg.Body = x;
                gmail.Send(msg);
            }



            return "successful";

            }
            catch (Exception e)
            {
                throw e;
            }
        
        }
        public static string ResetPassword(string ID)
        {
            var result = ResetPwdSendNotification(ID);
            return result;
        }

    }
}
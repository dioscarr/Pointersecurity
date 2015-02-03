using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SecurityMonitor.Models
{
    public class UserVM
    {
     [Key]
        public int UserID { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Phone { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string Zipcode { get; set;}
        //public string Username { get; set; }
        //public string Password { get; set; }
        //public DateTime created { get; set; }
        //public string isTempPWord { get; set; }


     public IEnumerable<UserBasicInfo> UserBasicInfo { get; set; }
    }

   public class  UserBasicInfo
   {
     //public string FirstName { get; set; }
     //public string LastName { get; set; }
       public string Email { get; set; }
     //public string Role { get; set; }
     //public DateTime LastActivity { get; set; }
   }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class TenantVM
    {        
        public string ID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Apt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime created { get; set; }
        public string isTempPWord { get; set; }
        public int BuildingID { get; set; }
        public int aptID { get; set; }
        public List<Module> modules { set; get; }
        public bool EmailNotification { get; set; }
        public bool GenerateAutomaticPassword { get; set; }
    }
}
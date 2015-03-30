using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class TenantSearch
    {
        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Phone { get; set; }
        public string Address { set; get; }
        public string Apt { set; get; }
        public string city { set; get; }
        public string State { get; set; }
        public string zipcode { set; get; }        
        public int BuildingID { set; get; }
        public int ApartmentID { set; get; }
        public string TenantID { set; get; }
        public string Userkey { set; get; }
        
    }
}
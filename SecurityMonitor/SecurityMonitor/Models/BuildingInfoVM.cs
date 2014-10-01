using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class BuildingInfoVM
    {
        public int ID { set; get;}
        public string BuildingName { set; get; }
        public String Address { set; get; }
        public string City { set; get; }
        public string States { set; get; }
        public string ZipCode { set; get; }
        public int NumberOfApart { set; get; }
        public string BuildingPhone { set; get; }
        public int ClientID { get; set; }
        public string Manager { set; get; }
    }
}
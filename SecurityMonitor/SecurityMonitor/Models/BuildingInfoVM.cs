using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class BuildingInfoVM
    {
        public int ID { set; get;}
        public int BuildingID { set; get; }
        [DisplayName("Building")] 
        public string BuildingName { set; get; }
         [DisplayName("Address")] 
        public String Address { set; get; }
         [DisplayName("CiTY")] 
        public string City { set; get; }
        public string States { set; get; }
        public string ZipCode { set; get; }
         [DisplayName("APT#")] 
        public int NumberOfApart { set; get; }
         [DisplayName("Phone")] 
        public string BuildingPhone { set; get; }
        public int ClientID { get; set; }
        public string Manager { set; get; }
        public int AptID { set; get; }
        public List<State> StatesList { set; get; }
        public List<ApartmentVM> AparmentVMs { set; get; }


    }
}
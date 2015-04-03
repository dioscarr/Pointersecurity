using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class ApartmentVM
    {
        public int ID { get; set; }
        public string ApartmentNumber  { get; set; }
        public string FloorNumber { get; set; }
        public int BuildingID { get; set; }//Passed by building View Model
        public List<TenantVM> ListOfTenants { get; set; }
        public List<Package> ListOfPackages { get; set; }

        

    
    }

   
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class ApartmentVM
    {
        public int ID { get; set; }
        public string ApartmentNumber  { get; set; }
        public string FloorNumber { get; set; }
        public int BuildingID { get; set; }//Passed by building View Model
        public List<ApartmentVM> Apartments { get; set; }
        public List<BuildingInfoVM> Buildings { get; set; }

    
    }

   
    
}
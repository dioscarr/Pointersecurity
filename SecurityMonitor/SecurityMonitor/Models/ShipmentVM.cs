using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class ShipmentVM
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool isNewUser { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ApartmentNumber { get; set; }
        public string UserID { get; set; }
        public int BuildingID { get; set; }
        public int ApartmentID { get; set; }

        public List<ReceivedPackage> Packages { get; set; }   
     
    }

    public class ReceivedPackage 
    {
        public string Service { get; set; }
        public string Trackingnumber { get; set; }
        public string PackageType { get; set; }
        public string shippingService { get; set; }
        public string Note { get; set; }
       
    }  
}
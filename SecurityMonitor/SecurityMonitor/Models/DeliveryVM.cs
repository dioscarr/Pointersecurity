using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doormandondemand;
namespace SecurityMonitor.Models
{
    public class DeliveryVM
    {
        public int ID { get; set; }
        public Shipment shipment { get; set; }
        public Package package { get; set; }
        public Otherusers otheruser { get; set; }
        public List<Tenant> TenantsOnApartment { get; set; }
        public List<SelectListItem> ApatList { set; get; }
        public List<SelectListItem> carrierService { get; set; }
        public List<SelectListItem> shippingService { get; set; }
        public List<SelectListItem> PackageType {get;set;}
        public BuildingUser buildinguser { get; set; }
        
       


    }
}
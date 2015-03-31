using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class ApartmentdeleteAll
    {
        public int ID { set; get; }
        public int BuildingID { set; get; }
        public List<Tenant> TenantList { set; get; }
        public List<Requests> RequestList { set; get; }
    }
}
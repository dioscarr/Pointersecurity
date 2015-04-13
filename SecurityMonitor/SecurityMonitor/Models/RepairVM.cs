using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doormandondemand;             

namespace SecurityMonitor.Models
{
    public class RepairVM
    {
        public String TenantID { get; set; }
        public List<RepairRequest> RepairRequest { get; set; }
        public List<SelectListItem> RequestCategories { get; set; }
        public List<SelectListItem> Urgency { get; set; }
        public Tenant tenant { get; set; }

    }
}
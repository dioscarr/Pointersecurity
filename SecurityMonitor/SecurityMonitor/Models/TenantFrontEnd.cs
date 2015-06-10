using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Doormandondemand;


namespace SecurityMonitor.Models
{
    public class TenantFrontEnd
    {
        public Tenant tenant { get; set; }
        public Apartment apartment { get; set; }
        public Buildings building { get; set; }
        public List<Module> modules { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class managementVM
    {
        public List<TenantVM> Tenants { set; get; }
        public IEnumerable<ReqType> ReqTypes { set; get; }
        public Tenant Tenant { set; get; }
        public ReqType ReqType { set; get; }
    }
}
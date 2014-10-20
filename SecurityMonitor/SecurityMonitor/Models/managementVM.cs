using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecurityMonitor.Models.EntityFrameworkFL;

namespace SecurityMonitor.Models
{
    public class managementVM
    {
        public List<TenantVM> Tenants { set; get; }
        public IEnumerable<ReqTypeVM> ReqTypes { set; get; }
        public Tenant Tenant { set; get; }
        public ReqType ReqType { set; get; }
    }
}
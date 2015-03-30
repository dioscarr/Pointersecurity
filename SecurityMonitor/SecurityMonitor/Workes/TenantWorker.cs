using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointerSecurityDataLayer;

namespace SecurityMonitor.Workes
{
    public class TenantWorker
    {
        PointerSecurityEntities db = new PointerSecurityEntities();
        public List<Tenant> TenantsOnApartment(int AptID)
        {
            try
            {
                var Tenants = db.Tenant.Where(c => c.aptID == AptID).ToList();
                return Tenants;
            }
            catch (Exception)
            {
                throw;
            }        
        }
    }
}
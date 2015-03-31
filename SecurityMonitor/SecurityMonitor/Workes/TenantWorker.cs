using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PointerSecurityAzure;

namespace SecurityMonitor.Workes
{
    public class TenantWorker
    {
        NewPointerdbEntities db = new NewPointerdbEntities();
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
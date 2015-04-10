using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Workes
{
    public class FindRoute
    {

        public static List<string> FindDefaultRoute(IList<string> RoleName)
        {
            List<string> myroute = new List<string>();
            foreach(var item in RoleName)
            {
            switch (item)
            { 
                case "Repair":
                    myroute.Add("SelectModuleIndex");
                    myroute.Add("Module");
                    return myroute;                
                case "Delivery":
                    myroute.Add("SelectModuleIndex");
                    myroute.Add("Module");
                    return myroute;
                case "Tenant":
                    myroute.Add("TenantProfile");
                    myroute.Add("Tenants");
                    return myroute;

            }
            }
            myroute.Add("Index");
            myroute.Add("Home");
            return myroute;
        }

    }
}
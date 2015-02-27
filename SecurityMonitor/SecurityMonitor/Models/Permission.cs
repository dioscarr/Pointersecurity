using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class Permission
    {

        public PermissionBase  permission { get; set; }
        public List<PermissionBase> ListOFPermissions { get; set; }

    }
    public class PermissionBase 
    {
        public int ID { get; set; }
        public bool Repair { get; set; }
        public bool Accesscontrol { get; set; }
        public bool News { get; set; }
        public bool Events { get; set; }
        public bool LegalDocs { get; set; }
        public bool Delivery { get; set; }
        public bool Contactbook { get; set; }
        public bool Apartment { get; set; }
        public bool BasicFeatures { get; set; }
    
    }
}
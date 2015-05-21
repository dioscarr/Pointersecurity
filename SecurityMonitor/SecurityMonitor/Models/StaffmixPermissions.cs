using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class StaffmixPermissions
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IList<string> PermisionNames { get; set; }

    }

    public class staff
    {
        
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class BuildingUserMappingVM
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public string UserID { get; set; }

    }
}
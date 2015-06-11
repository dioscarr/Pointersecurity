using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
 
    public class PartialModuleandCounts
    {
        PointerdbEntities db = new PointerdbEntities();

        public string ServiceName { get; set; }
        public int TotalOpenCount { get; set; }
    }
}
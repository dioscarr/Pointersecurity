using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class ActiveManagerVM
    {
        public int Id { get; set; }
        public string ManagerID { get; set; }
        public int BuildingID { get; set; }
        public Manager myManager { get; set; }

        public virtual Manager Manager { get; set; }
        public virtual Buildings Buildings { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class ModuleSelectVM
    {
        public int ID { get; set; }
        public IList<string> AllRoles { get; set; }
        public BuildingUser BuildingUser { get; set; }


    }
}
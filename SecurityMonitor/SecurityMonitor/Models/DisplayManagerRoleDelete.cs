using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class DisplayManagerRoleDelete
    {
        public int ID { get; set; }
        public List<ManagerBuilding> buildingmanager{ get; set; }
        public List<Manager> MyManagers { get; set; }
        public List<Buildings> MyBuildings { get; set; }
        public string other { get; set; }
        public AspNetRoles MyRoles { get; set; }

        
    }
}
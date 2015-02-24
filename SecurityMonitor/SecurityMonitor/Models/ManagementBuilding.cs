using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecurityMonitor.Models;


namespace SecurityMonitor.Models
{
    public class ManagementBuilding
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public List<ManagerVM> managerVM { get; set; }
        public ManagerVM MyManagerVM { get; set; }
        public int buildingID { get; set; }

    }
}
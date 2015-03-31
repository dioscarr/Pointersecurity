using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class DisplayClientBuilding
    {
        public int ID { set; get; }
        public int BuildingID { set; get; }
        public string ManagerID { set; get; }        
        public int ClientID { set; get; }
        public int selectedbuilding { set; get; }
        public Manager Manager { set; get; }
        public List<Clients> clients { set; get; }
        public List<Buildings> buildings { set; get; }
        public List<ManagerBuilding> ManagerBuildings { set; get; }
        public List<Buildings> BuildingsOnDeck { set; get; }
       

    }
}
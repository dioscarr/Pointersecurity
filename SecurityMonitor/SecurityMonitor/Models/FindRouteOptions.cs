using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class FindRouteOptions
    {
        public int BuildingID { get; set; }
        public int ApartmentID { get; set; }
        public int ClientID { get; set; }
        public string UserID { get; set; }
    }
}
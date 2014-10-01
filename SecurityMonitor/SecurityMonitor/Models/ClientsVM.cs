using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class ClientsVM
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public int BuildingCount { get; set; }
        public List<ClientsVM> ListOfClients { get; set; }
    }
}

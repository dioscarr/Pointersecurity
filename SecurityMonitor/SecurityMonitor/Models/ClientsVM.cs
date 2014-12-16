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
        public string Address { set; get; }
        public string city { set; get; }
        public string State { set; get; }
        public string zipcode { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }
        public string Email { set; get; }
        public int BuildingCount { get; set; }
        public List<ClientsVM> ListOfClients { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class ClientsVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Address Line 1 is required")]
        public string ClientName { get; set; }
        public string Address { set; get; }
        public string city { set; get; }
        public string State { set; get; }
        public string zipcode { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }
        public string Email { set; get; }
        public int BuildingCount { get; set; }
        public ClientsVM Client { set; get; }
        public List<ClientsVM> ListOfClients { get; set; }
        public List<State> States { set; get; }
 
    }

    public class State
    {
        public int ID { set; get; }
        public string value { set; get; }
        public string myState { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class RequestVM
    {

        
        public int ID { get; set; }
        public string RequestType { get; set; }
        public string Description { get; set; }
        public DateTime FromDate {get;set;}
        public DateTime ToDate {get;set;}
        public string PIN {get;set;}
        public int TenantID { get; set; }
    }
}

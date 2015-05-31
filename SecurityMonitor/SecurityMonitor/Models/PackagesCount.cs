using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class PackagesCount
    {
        public int UPSCount { get; set; }
        public int FedExCount { get; set; }
        public int DHLCount { get; set; }
        public int USPSCount { get; set; }
        public int OtherCount { get; set; }
        public int TNTCount { get; set; }

       
    }
}
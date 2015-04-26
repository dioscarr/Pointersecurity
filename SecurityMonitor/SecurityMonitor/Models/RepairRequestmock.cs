using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class RepairRequestmock
    {
        public DateTime RequestedDate { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public string  RequestNumber { get; set; }
        public string Status { get; set; }
    }
}
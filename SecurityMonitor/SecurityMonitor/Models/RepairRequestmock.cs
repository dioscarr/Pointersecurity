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
        public string Urgency { get; set; }
        public string Category { get; set; }
        public string PhotoUrl { get; set; }
        public string CName { get; set; }
        public string CEmail { get; set; }
        public string CPhone { get; set; }
        public string PName { get; set; }
        public string PEmail { get; set; }
        public string PPhone { get; set; }
        public string AssignToID { get; set; }
        public string AssignedFullName { get; set; }
        public string assignContractorID { get; set; }
        public string ContractorFullName { get; set; }

    }
}
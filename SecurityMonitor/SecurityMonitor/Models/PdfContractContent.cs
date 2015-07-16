using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class PdfContractContent
    {
        public string Address { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string  Issued { get; set; }
        public string primaryContact { get; set; }
        public string problem { get; set; }
        public string  TenantInstruction { get; set; }
        public string OfficeNotes  { get; set; }
        public string RequestNumber  { get; set; }
        public string  OfficeInfo { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhone { get; set; }


    }
}
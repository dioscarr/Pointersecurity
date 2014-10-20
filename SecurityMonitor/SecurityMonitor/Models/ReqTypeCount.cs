using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class ReqTypeCount
    {
        public string key { get; set; }
        public int AccessControl { get; set; }
        public string AccessControlName { get; set; }
        public int Delivery { get; set; }
        public int PackagePick { get; set; }
        public int Others { get; set; }
    }
}
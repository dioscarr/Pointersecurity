using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class RequestForTodayVM
    {
        public int ID {set;get;}
        public string Type {set;get;}
        public string Description {set;get;}
        public DateTime From {set;get;} 
        public DateTime To {set;get;}

    }
}
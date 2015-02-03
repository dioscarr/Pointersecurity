using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class JsonVM
    {
        public string Controller { get; set; }
        public string TextboxLabel { set; get; }
        public List<dropdownoptions> Options { set; get; }
    }
    public class dropdownoptions {
        public string Text { set; get; }
        public string Value { set; get; }
    }
}

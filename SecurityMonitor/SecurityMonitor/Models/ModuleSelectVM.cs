﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class ModuleSelectVM
    {
        public int ID { get; set; }
        public IList<string> AllRoles { get; set; }


    }
}
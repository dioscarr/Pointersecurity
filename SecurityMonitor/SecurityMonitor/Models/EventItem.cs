using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FileHelpers;
namespace SecurityMonitor.Models
{
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    public class EventItem
    {
        public string AparmentNumber { get; set; }
        public string Floor { get; set; }
        public int BuildingID { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class SelectedDateRequest
    {
        public int ID { get; set; }
        public string ReqType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<DateAndCount> CollectionOfDates { get; set; }
        public DateAndCount DateAndTime { get; set; }
        

     
    }


    public class DateAndCount
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }

    }
}
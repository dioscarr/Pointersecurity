using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;
using SecurityMonitor.Models;

namespace SecurityMonitor.Workes
{
    public class Calendarworkers
    {

        PointerdbEntities db = new PointerdbEntities ();

        public List<SelectedDateRequest> loadSelectedRequests(string SelectedDate) 
        {

            var ActualDate = DateTime.Parse(SelectedDate);
            var DateAfter = DateTime.Parse(SelectedDate).AddDays(1);
            var RSD = db.Requests.Where(c => c.FromDate >= ActualDate && c.FromDate < DateAfter || c.FromDate < ActualDate && c.ToDate >= ActualDate)
                .Select(r => new SelectedDateRequest { ID = r.ID, ReqType = r.RequestType }).ToList();
            return RSD;
        
        }

        public SelectedDateRequest loadForTheMonth()
        {
            var CurrentDay = DateTime.Today;

            var FirstDay = new DateTime(CurrentDay.Year, CurrentDay.Month, 1);
            var LastDay = FirstDay.AddMonths(1).AddDays(-1);
            var RSD = db.Requests.Where(r => r.FromDate >= FirstDay && r.ToDate <= LastDay || r.FromDate>=FirstDay && r.ToDate>=LastDay || r.FromDate<= FirstDay && r.ToDate > FirstDay)                
                .Select(r => new SelectedDateRequest 
                { ID = r.ID, 
                    ReqType = r.RequestType,
                  FromDate = r.FromDate.Month + "/" + r.FromDate.Day + "/" + r.FromDate.Year,
                  ToDate = r.ToDate.Month + "/" + r.ToDate.Day + "/" + r.ToDate.Year
                }).ToList();
            var RSDC = new SelectedDateRequest();
            List<DateAndCount> arrayOfDate = GetDateRange(RSD, FirstDay, LastDay);//create multiple dates base on a date range                
            RSDC.CollectionOfDates = arrayOfDate;
            RSD.Add(RSDC);
            return RSDC;
        }


        public List<DateAndCount> GetDateRange(List<SelectedDateRequest> model, DateTime FirstDay, DateTime LastDay)
        {
            List<DateAndCount> x = new List<DateAndCount>(31);
            DateAndCount xx = new DateAndCount();

            for (var i = 0; i < 30; i++)
            {
                xx.Date = DateTime.Parse("1/1/1111");
                x.Insert(i, xx);
            }
            
            List<DateTime> x1 = new List<DateTime>();
            foreach(var itemDate in model)
            {
                var startDate = DateTime.Parse(itemDate.FromDate);
                var endDate = DateTime.Parse(itemDate.ToDate);

                if (endDate < startDate)
                    throw new ArgumentException("endDate must be greater than or equal to startDate");
                while (startDate <= endDate)
                {
                    //var dateFormat = startDate.Year + "-" + startDate.Month + "-" + startDate.Day;
                    if (startDate >= FirstDay && startDate <= LastDay)
                    {x1.Add(startDate);}                   
                    startDate = startDate.AddDays(1);
                }           
            };
            //loop throught all of the created days
            foreach (var iteDate in x1)
            {
                switch (iteDate.Day.ToString())
                    {
                        case "8":
                            if (!x.Exists(c => c.Date == iteDate))
                            {
                                xx.Date = iteDate;
                                xx.Count = 1;
                                x.Insert(8, xx);
                                break;
                            }
                            var xxx8 = x.Where(c => c.Date == iteDate).FirstOrDefault().Count;
                            xxx8++;
                            xx.Date = iteDate;
                            xx.Count = xxx8;
                            x.Insert(8, xx);
                            break;

                        case "22":
                            if (!x.Exists(c => c.Date == iteDate))
                            {
                                xx.Date = iteDate;
                                xx.Count = 1;
                                x.Insert(22, xx);
                                break;
                            }
                            var xxx22 = x.Where(c => c.Date == iteDate).FirstOrDefault().Count;
                            xxx22++;
                            xx.Date = iteDate;
                            xx.Count = xxx22;
                            x.Insert(22, xx);
                            break;
                    }
            }
            DateTime t = DateTime.Parse("1/1/0001");

            
            return x;
        }

       
        //private List<DateAndCount> resolvemonth(List<DateTime> model) { 
        
        //    List<DateAndCount> x = new List<DateAndCount>(31);
        //    DateAndCount xx = new DateAndCount();

        //    for(var i=0; i<30; i++)
        //    {
        //        xx.Date = DateTime.Parse("1/1/1111");
        //        x.Insert(i, xx);
        //    }

        //    foreach(var iteDate in model)
        //    {
        //        switch (iteDate.Day.ToString())
        //    {
        //        case "1":
        //            if (!x.Exists(c => c.Date == iteDate))
        //            {
        //                xx.Date = iteDate;
        //                xx.Count = 0;
        //                x.Insert(1,xx);
        //                break;
        //            }
        //            var xxx = x.Where(c => c.Date == iteDate).FirstOrDefault().Count;
        //            xxx++;
        //            xx.Date = iteDate;
        //            xx.Count = xxx;
        //            x.Insert(1, xx); 
        //            break;
        //    }  
        //    }
        //    return x;
        
        //}
    }
}
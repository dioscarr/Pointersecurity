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

        public List<SelectedDateRequest> loadForTheMonth()
        {
            var CurrentDay = DateTime.Today;

            var FirstDay = new DateTime(CurrentDay.Year, CurrentDay.Month, 1);
            var LastDay = FirstDay.AddMonths(1).AddDays(-1);
            var RSD = db.Requests.Where(r => r.FromDate >= FirstDay && r.ToDate >= LastDay)                
                .Select(r => new SelectedDateRequest { ID = r.ID, ReqType = r.RequestType}).ToList();
            return RSD;
        }
    }
}
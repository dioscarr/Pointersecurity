using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;


namespace SecurityMonitor.Models
{
    public class UserActivityLogVM
    {

        public List<ActivityLog> UserActivites { get; set; }
        public UserVM userVM { get; set; }
        
    }

    public class ActivityLog
    {
        public int ID { set; get; }
        public String UserID { get; set; }
        public string FunctionPerformed { get; set; }
        public DateTime DateCreated { get; set; }
        public string Message { get; set; }
        public BuildingInfoVM BuildingInfo { get; set; }
    }
}
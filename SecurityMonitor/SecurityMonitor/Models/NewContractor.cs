using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class NewContractor
    {
        public string ASPNETUSERID { get; set; }
        public string Email { get; set; }
        public string CName { get; set; }
        public string CAddress { get; set; }
        public string CCity { get; set; }
        public string CState { get; set; }
        public string CZipcode { get; set; }
        public string CMainPhone { get; set; }
        public string PName { get; set; }
        public string PPhone { get; set; }
        public bool SendNewPassword { get; set; }
        public string Comments { get; set; }       
        public string CatName { get; set; }
        public int BuildingID { get; set; }


    }


}
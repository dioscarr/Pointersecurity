using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models.EntityFrameworkFL;
using SecurityMonitor.Models;

namespace SecurityMonitor.Controllers
{
    public class ManagementController : Controller
    {
        PointersecurityEntities1 db = new PointersecurityEntities1();
       [HttpGet]
        public ActionResult PendingModules()
        {
            List<PendingModules> ObjPendingMoodule = db.PendingModules.ToList();

            return View(ObjPendingMoodule);
        }

       [HttpPost]
       public ActionResult approve(int BuildingID, int ListOfModuleID, string ServiceName, int PendingID)
       {
          

           Module ObjModule = new Module()
           {
               BuildingID = BuildingID,
               ListOfModuleID = ListOfModuleID,
               ServiceName = ServiceName 
           };
           db.Module.Add(ObjModule);

           PendingModules ObjPendingModule = db.PendingModules.Find(PendingID);
           db.PendingModules.Remove(ObjPendingModule);
           db.SaveChanges();
           return RedirectToAction("PendingModules"); 
       }

       [HttpPost]
       public ActionResult denied(int pendingID)
       {
           PendingModules ObjPendingModule = db.PendingModules.Find(pendingID);
           db.PendingModules.Remove(ObjPendingModule);
           db.SaveChanges();
           return RedirectToAction("PendingModules");
       }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PointerSecurityDataLayer;
using SecurityMonitor.Models;

namespace SecurityMonitor.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ModuleController : Controller
    {
        PointerSecurityEntities db = new PointerSecurityEntities();



        //select available modeule
        public ActionResult SelectModuleIndex(string UserID)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            DisplayManagerRoleDelete ObjRole = new DisplayManagerRoleDelete();
            ModuleSelectVM ObjS = new ModuleSelectVM();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           ObjS.AllRoles = userManager.GetRoles(UserID);

           return View(ObjS);
        }

        // GET: Module
        public PartialViewResult ModuleView(int BuildingID)
        {
            List<Module> partialmodule = db.Module.Where(model=>model.BuildingID==BuildingID).ToList();
            ViewBag.ModuleCount = db.Module.Where(model => model.BuildingID == BuildingID).Count();
            return PartialView(partialmodule);
        }

        //Adding Module page 
        //this page contains a dropdownlist to select the Service/module
       [HttpGet]
        public ActionResult ModuleAdd(int BuildingID)
        {
            
            PendingModules ObjModule = new PendingModules();
            ObjModule.BuildingID = BuildingID;
            ViewBag.ListOfModules = db.ListOfModule.Select(c => new SelectListItem { Text = c.ModuleName, Value= c.ID.ToString()}).ToList();
            return View(ObjModule);
        }
        
       [HttpPost]
       public ActionResult ModuleAdd(PendingModules model, int BuildingID)
       {
           if (!ModelState.IsValid)
           {
               return View(model);
           }
           model.ServiceName = db.ListOfModule.Find(model.ListOfModuleID).ModuleName;
           var HasSomething = db.Module.Where(m=>m.BuildingID==BuildingID).Any(m=>m.ListOfModuleID == model.ListOfModuleID);
           if (HasSomething)
           {
               ViewBag.DuplicateMessage =model.ServiceName + " already exist and connot be added. Please select a different service and click send for approval.";
               ViewBag.ListOfModules = db.ListOfModule.Select(c => new SelectListItem { Text = c.ModuleName, Value = c.ID.ToString() }).ToList();
               return View(model);
               //return RedirectToAction("ModuleAdd",new {BuildingID=BuildingID});
           }
          
           db.PendingModules.Add(model);
           db.SaveChanges();
           return RedirectToAction("buildingProfile", "building", new { BuildingID = BuildingID });
       }

        [HttpGet]
        [Authorize(Roles="Repair")]
       public ActionResult RepairIndex()
       {
           return View();
       }

        public ActionResult DeliveryDisplay()
        {

           var UserID = User.Identity.GetUserId();

            return View();
        }
    }
}

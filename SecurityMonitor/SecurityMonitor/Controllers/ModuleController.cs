using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Doormandondemand;
using SecurityMonitor.Models;
using SecurityMonitor.Workes;
using SecurityMonitor;


namespace SecurityMonitor.Controllers
{
    [Authorize(Roles = "Admin, Delivery")]
    public class ModuleController : Controller
    {
        PointerdbEntities db = new PointerdbEntities();



        //select available modeule
    
        public ActionResult SelectModuleIndex(string UserID)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            DisplayManagerRoleDelete ObjRole = new DisplayManagerRoleDelete();
            ModuleSelectVM ObjS = new ModuleSelectVM();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           ObjS.AllRoles = userManager.GetRoles(UserID);

           var LoggedUserID = User.Identity.GetUserId();
           ObjS.BuildingUser = db.BuildingUser.Where(c => c.UserID == LoggedUserID).FirstOrDefault();


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
        public ActionResult Delivery()
        {

            DeliveryVM dvm = new DeliveryVM();
            var LoggedUserID = User.Identity.GetUserId();
            dvm.buildinguser = db.BuildingUser.Where(c => c.UserID == LoggedUserID).FirstOrDefault();
            dvm.ApatList = db.Apartment.Where(c => c.BuildingID == dvm.buildinguser.BuildingID).Select(c => new SelectListItem { Text = c.ApartmentNumber, Value= c.ID.ToString() }).ToList();
            dvm.PackageType = db.PackageType.Select(c=> new SelectListItem{ Text = c.PackageType1, Value=c.ID.ToString()}).ToList();
            dvm.carrierService = db.ShippingCarrier.Select(c => new SelectListItem { Text = c.Services, Value=c.ID.ToString() }).ToList();
            dvm.shippingService = db.ShippingService.Select(c => new SelectListItem {Text = c.Service, Value = c.ID.ToString() }).ToList();

            return View(dvm);
        }
        [HttpPost]
        public ActionResult Delivery(DeliveryVM model, string Name)
        {
            //if user is not found in the directory then the form should be filled out in the UI
            if (model.otheruser != null)
            {
                Otherusers newUser = new  Otherusers();
                newUser =  model.otheruser;
                ApplicationDbContext context = new ApplicationDbContext();
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                string password = PasswordGenerator.GeneratePassword("8").ToString();

              
                var UserID = RegisterUsers.InsertUser(newUser.Email, newUser.Phone, password);
                var result = RegisterUsers.InserToRole("Otheruser", UserID);

                //TODO: register OtherUsers in db.
                //TODO: create table in db first.
               
            
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddShipemnt(ShipmentVM model)
        {

            if (model != null)
            {
                if (model.isNewUser == true)
                {

                }
                else 
                {
                    DeliveryWorker DW = new DeliveryWorker();
                    var status = DW.AddShipment(model, User.Identity.GetUserId());

                    var jsonObj = Json(model);

                    var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
                    hubContext.Clients.All.incomingPackageNotification(model.BuildingID, model.ApartmentID, jsonObj);
                }

                
            
            }
            return RedirectToAction("Delivery");
        }
        public ActionResult PackageSearch()
        { 

            return View();
        }
    }
}

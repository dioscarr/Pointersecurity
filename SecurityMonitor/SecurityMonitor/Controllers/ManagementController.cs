﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models.EntityFrameworkFL;
using SecurityMonitor.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;


namespace SecurityMonitor.Controllers
{
    [Authorize(Roles="Admin")]
    public class ManagementController : Controller
    {
        PointersecurityEntities1 db = new PointersecurityEntities1();
        public ActionResult Index()
        {

            return View();
        }



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

        //================Role Management===================================================
       [HttpGet]
       public ActionResult RoleIndex()
       {
           var ObjRolesList = db.AspNetRoles.ToList();
           return View(ObjRolesList);
       }
       [HttpGet]
       public ActionResult Addrole()
       {
           var ObjRole = new AspNetRoles();
           return View(ObjRole);
       }
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult Addrole(AspNetRoles model)
       {
           ApplicationDbContext context = new ApplicationDbContext();

           RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
           var roleresult = RoleManager.Create(new IdentityRole(model.Name));
           return RedirectToAction("RoleIndex");    
       }
       [HttpGet]
       public ActionResult RegisterManager() 
       {
           return View();
       }
       [HttpGet]
       public ActionResult DeleteRole(string RoleName)
       {
           var ObjRole = db.AspNetRoles.Where(c => c.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

           return View(ObjRole);
       }
       [HttpPost]
       public ActionResult DeleteRole(AspNetRoles model, string RoleName)
       {
           var ObjRole = db.AspNetRoles.Where(c => c.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
          
           db.AspNetRoles.Remove(ObjRole);
           db.SaveChanges();
           return RedirectToAction("RoleIndex");
       }

       [HttpGet]
       public ActionResult ManageUserRoles()
       {
           ApplicationDbContext dbContextRole = new ApplicationDbContext();
           // prepopulat roles for the view dropdown
           var list = dbContextRole.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
           ViewBag.Roles = list;
           return View();
       }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult RoleAddToUser(string UserName, string RoleName)
       {
          
            ApplicationDbContext context = new ApplicationDbContext();
            PointersecurityEntities1 db = new PointersecurityEntities1();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var currentUserID = db.AspNetUsers.FirstOrDefault(c => c.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).Id;

            var roleresult = UserManager.AddToRole(currentUserID, RoleName);
           
            ViewBag.ResultMessage = "Role created successfully !";
           
           // prepopulat roles for the view dropdown
           var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
           ViewBag.Roles = list;

           return View("ManageUserRoles");
       }

       [Authorize(Roles = "Admin")]
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult GetRoles(string UserName)
       {
           if (!string.IsNullOrWhiteSpace(UserName))
           {
               ApplicationDbContext context = new ApplicationDbContext();
               var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
               var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
               var currentUserID = db.AspNetUsers
                   .FirstOrDefault(c => c.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).Id;

              
               ViewBag.RolesForThisUser = UserManager.GetRoles(currentUserID);
               var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
          
               ViewBag.Roles = list;
           }
           return View("ManageUserRoles");
       }

       [HttpPost]
       [Authorize(Roles = "Admin")]
       [ValidateAntiForgeryToken]
       public ActionResult DeleteRoleForUser(string UserName, string RoleName)
       {
           ApplicationDbContext context = new ApplicationDbContext();
           var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           var currentUserID = db.AspNetUsers
                   .FirstOrDefault(c => c.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).Id;

           if (UserManager.IsInRole(currentUserID, RoleName))
           {
               UserManager.RemoveFromRole(currentUserID, RoleName);
               
               ViewBag.ResultMessage = "Role removed from this user successfully !";
           }
           else
           {
               ViewBag.ResultMessage = "This user doesn't belong to selected role.";
           }
           ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
           var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
          
           ViewBag.Roles = list;


           return View("ManageUserRoles");
       }

       [HttpGet]
       public ActionResult AddManager()
       {
          var ObjManager = new ManagerVM();
          return View(ObjManager); 
       }

       [HttpPost]
       [AllowAnonymous]
       [ValidateAntiForgeryToken]
       public async Task<ActionResult> AddManager(ManagerVM model)
       {

           ApplicationDbContext context = new ApplicationDbContext();

           var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           PasswordHasher hasher = new PasswordHasher();
          
           ApplicationUser AppUser = new ApplicationUser(){
           Id = Guid.NewGuid().ToString(),
           Email = model.Email,
           UserName = model.Username,
           SecurityStamp = Guid.NewGuid().ToString(),
           PhoneNumber = model.Phone,
           LockoutEnabled = false,
           AccessFailedCount = 0,
           PhoneNumberConfirmed = false,
           TwoFactorEnabled = false,
           EmailConfirmed = false,
           PasswordHash = hasher.HashPassword(model.Password)
          };
           string[] FullName = model.FullName.Split(new string[] {" "}, StringSplitOptions.None);
           Manager mgr = new Manager() {  
                                       ID = AppUser.Id,  
                                       FirstName=FullName[0].ToString(), 
                                       LastName = FullName[1].ToString(),  
                                       Phone = model.Phone};
           db.Manager.Add(mgr);
           context.Users.Add(AppUser);
          
           await context.SaveChangesAsync();
           await db.SaveChangesAsync();

           RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
           if(!RoleManager.RoleExists("Manager"))
           {
            var roleresult = RoleManager.Create(new IdentityRole("Manager"));
           }
           var Result = UserManager.AddToRole(AppUser.Id, "Manager");

           DisplayClientBuilding ObjDCB = new DisplayClientBuilding(){
            ManagerID = AppUser.Id};

           return RedirectToAction("SelectBuilding", ObjDCB);
       }
      
        [HttpGet]
       public ActionResult SelectBuilding(DisplayClientBuilding model)
       {
           model.Manager = db.Manager.Find(model.ManagerID);
         
            model.clients = db.Clients.ToList();
            //get all entries that belong to the current Manager on the managerbuilding table
           
            model.ManagerBuildings = db.ManagerBuilding.Where(c => c.UserID == model.ManagerID).ToList();
            foreach (var item in model.ManagerBuildings)
            {    //building that exist on managerbuilding deck
                Buildings Addit = db
               .Buildings
               .First(c => c.ID == @item.BuildingID);
                //note: current user records

                model.BuildingsOnDeck.Add(Addit);
            } 
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectBuilding(DisplayClientBuilding model, int ClientID)
        {
            
            model.ClientID = ClientID;
            model.Manager = db.Manager.Find(model.ManagerID);
            model.clients= db.Clients.ToList();
            
            if (model.clients == null){ return View(model); }
            
            model.buildings = db.Buildings.Where(b => b.Clients.ID == ClientID).ToList();

            //get all entries that belong to the current Manager on the managerbuilding table
            model.ManagerBuildings = db.ManagerBuilding.Where(c => c.UserID == model.ManagerID).ToList();
            foreach (var item in model.ManagerBuildings)
            {    //building that exist on managerbuilding deck
                Buildings removeit = db
               .Buildings
               .First(c => c.ID == @item.BuildingID);
                //note: current user records
                model.buildings.Remove(removeit);
                model.BuildingsOnDeck.Add(removeit);
            } 
            
            return View(model);
        }
        public ActionResult ManagerBuilding(DisplayClientBuilding model, int BuildingID)
        {
            //1. set ManagerBuilding Obj and save it in db
            //2. load clients, Buildings, assigned buildings to the current manager
            //3.
            model.Manager = db.Manager.Find(model.ManagerID);
            ManagerBuilding ObjMB = new ManagerBuilding
            {
                BuildingID =BuildingID,
                UserID = model.ManagerID
            };
            if (db.ManagerBuilding.Where(c => c.UserID == model.ManagerID && c.BuildingID == BuildingID).FirstOrDefault() == null)
            {
                db.ManagerBuilding.Add(ObjMB);
                db.SaveChanges();
            }
            //get All clients
            model.clients = db.Clients.ToList();
            //get all entries that belong to the current Manager on the managerbuilding table
            model.ManagerBuildings = db.ManagerBuilding.Where(c => c.UserID == model.ManagerID).ToList();
            //selected client's building
            var selectedclitentbuildings = db.Buildings
                .Where(c => c.Clients.ID == model.ClientID).ToList();

            model.buildings = db.Buildings.Where(c=>c.ClientID==model.ClientID).ToList();
            if (selectedclitentbuildings != null)
            {
                foreach(var item in model.ManagerBuildings )
                {    //building that exist on managerbuilding deck
                     Buildings removeit =db
                    .Buildings
                    .First(c=>c.ID == @item.BuildingID);
                    //note: current user records
                     model.buildings.Remove(removeit);
                     model.BuildingsOnDeck.Add(removeit);
                } 
            }
            
            return View("SelectBuilding",model);
        }
        [HttpPost]
        public ActionResult ManagerBuildingDelete(DisplayClientBuilding model)
        {

            ManagerBuilding MB = db.ManagerBuilding
                                    .Where(c => c.BuildingID == model.BuildingID && c.UserID == model.ManagerID)
                                    .FirstOrDefault();
            db.ManagerBuilding.Remove(MB);
            db.SaveChanges();
            
            return View("SelectBuilding", model);

        }

       public RoleManager<IdentityRole> RoleManager { get; set; }
       
    }
}
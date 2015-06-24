using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doormandondemand;
using SecurityMonitor.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity.Validation;


namespace SecurityMonitor.Controllers
{
    [Authorize(Roles="Admin")]
    public class ManagementController : Controller
    {
       PointerdbEntities db = new PointerdbEntities();
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
       public ActionResult DeleteRole(string RoleName, string RoleID)
       {
           ApplicationDbContext context = new ApplicationDbContext();
           DisplayManagerRoleDelete ObjRole = new DisplayManagerRoleDelete();
           ObjRole.MyRoles =  db.AspNetRoles.Where(c => c.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
          
           var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
           var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
          // ObjRole.buildingmanager = db.ManagerBuilding.Where(c => c.UserID == c.Manager.ID).ToList();           
           
           List<Manager> list1 = new List<Manager>();
           List<Buildings> ListBuilding = new List<Buildings>();

          // find list all manager
           var objAllManager =  db.Manager.ToList();
          // loop through all manager
           foreach(var item in objAllManager)
           {//list of role that belong to the current user
               var ListofRoleName = userManager.GetRoles(item.AspNetUsers.Id).ToList();
              //loop through all names 
               foreach (var individualRoleName in ListofRoleName)
               {//check if the current role  matches the role passed
                   if (individualRoleName == RoleName)
                   { //if match then load manager 
                       Manager ObjManager = new Manager();
                       ObjManager = db.Manager.Find(item.AspNetUsers.Id);
                      //add manager to myManager List<>
                      // ObjRole.MyManagers.Add(ObjManager);
                       var objmb = db.ManagerBuilding.Where(c => c.UserID == item.AspNetUsers.Id).ToList();
                        foreach(var item2 in objmb)
                        {
                            ListBuilding.Add(item2.Buildings);
                        }

                      list1.Add(ObjManager);
                   }
               }
           }

           ViewBag.Managers = list1;
           ViewBag.Buildings = ListBuilding;



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
           ViewBag.clientlist = db.Clients.Select(c => new SelectListItem { Text = c.ClientName, Value = c.ID.ToString() }).ToList();
           return View();
       }
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult RoleAddToUser(string UserName, string RoleName, string FirstName, string LastName, string Phone)
       {
          
            ApplicationDbContext context = new ApplicationDbContext();
            PointerdbEntities db = new PointerdbEntities();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var CurrentUser = db.AspNetUsers.FirstOrDefault(c => c.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

            
            if (CurrentUser == null)
            {
                 ViewBag.Roles  = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                
                ViewBag.clientlist = db.Clients.Select(c => new SelectListItem { Text = c.ClientName, Value = c.ID.ToString() }).ToList();
                ViewBag.ResultMessage = "There is no user with this Username !";
                return View("ManageUserRoles");
            }
            var currentUserID = CurrentUser.Id;
            var roleresult = UserManager.AddToRole(currentUserID, RoleName);
           
            ViewBag.ResultMessage = "Role created successfully !";

            var ObjisitManager = db.Manager.Find(currentUserID);
            Manager ObjManager = new Manager();
            if (ObjisitManager != null)
            {//update
                // ObjManager.ID = ObjisitManager.ID;
                //ObjManager.FirstName = ObjisitManager.FirstName;
                //ObjManager.LastName = ObjisitManager.LastName;
                //ObjManager.Phone = ObjisitManager.Phone;
                //db.Manager.Attach(ObjManager);
                //var Entry = db.Entry(ObjManager);
                //Entry.Property(c => c.FirstName).IsModified = true;
                //Entry.Property(c => c.LastName).IsModified = true;
                //Entry.Property(c => c.Phone).IsModified = true;
            }
            else 
            {//insert
                ObjManager.ID = currentUserID;
                ObjManager.FirstName = FirstName;
                ObjManager.LastName = LastName;
                ObjManager.Phone = Phone;
                db.Manager.Add(ObjManager);
                db.SaveChanges();
            }



           

           // prepopulat roles for the view dropdown
          ViewBag.Roles  = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
           ViewBag.clientlist = db.Clients.Select(c => new SelectListItem { Text = c.ClientName, Value = c.ID.ToString() }).ToList();
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
               var CurrentUser = db.AspNetUsers.FirstOrDefault(c => c.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

               if (CurrentUser == null)
               {
                   // prepopulat roles for the view dropdown
                   ViewBag.Roles = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                   ViewBag.clientlist = db.Clients.Select(c => new SelectListItem { Text = c.ClientName, Value = c.ID.ToString() }).ToList();
      
                   ViewBag.ResultMessage = "There is no user with this Username !";
                   return View("ManageUserRoles");
               }
               var currentUserID = CurrentUser.Id;
               ViewBag.RolesForThisUser = UserManager.GetRoles(currentUserID);
               // prepopulat roles for the view dropdown
               ViewBag.Roles = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
               ViewBag.clientlist = db.Clients.Select(c => new SelectListItem { Text = c.ClientName, Value = c.ID.ToString() }).ToList();
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
           var CurrentUser = db.AspNetUsers.FirstOrDefault(c => c.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));


           if (CurrentUser == null)
           {
               // prepopulat roles for the view dropdown
               ViewBag.Roles = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
               ViewBag.clientlist = db.Clients.Select(c => new SelectListItem { Text = c.ClientName, Value = c.ID.ToString() }).ToList();
      
               ViewBag.ResultMessage = "There is no user with this Username !";
               return View("ManageUserRoles");
           }
           var currentUserID = CurrentUser.Id;
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
           // prepopulat roles for the view dropdown
           ViewBag.Roles = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
           ViewBag.clientlist = db.Clients.Select(c => new SelectListItem { Text = c.ClientName, Value = c.ID.ToString() }).ToList();
      
           return View("ManageUserRoles");
       }       
       [HttpGet]
       public ActionResult AddManager()
       {
          var ObjManager = new ManagerVM();
          ViewBag.clientList = db.Clients
              .Select(c => new SelectListItem { Text = c.ClientName,
                                                Value = c.ID.ToString() }).ToList();
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
                                       Phone = model.Phone,
                                        ClientID = model.clientID};
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
            ManagerID = AppUser.Id, ClientID = model.clientID};

           return RedirectToAction("SelectBuilding", ObjDCB);
       }      
       [HttpGet]
       public ActionResult SelectBuilding(DisplayClientBuilding model)
        {
           model.Manager = db.Manager.Find(model.ManagerID);
         
            model.clients = db.Clients.Where(c=>c.ID == model.ClientID).ToList();
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
            model.clients= db.Clients.Where(c=>c.ID == model.ClientID).ToList();
            
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
                UserID = model.ManagerID,
                 ManagerID = model.ManagerID
            };
            if (db.ManagerBuilding.Where(c => c.UserID == model.ManagerID && c.BuildingID == BuildingID).FirstOrDefault() == null)
            {
                db.ManagerBuilding.Add(ObjMB);
                db.SaveChanges();
            }
            //get All clients
            model.clients = db.Clients.Where(c => c.ID == model.ClientID).ToList();
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
       public ActionResult ManagerBuildingDelete(DisplayClientBuilding model, int BuildingID2)
        {

            ManagerBuilding MB = db.ManagerBuilding
                                    .Where(c => c.BuildingID == BuildingID2 && c.UserID == model.ManagerID)
                                    .FirstOrDefault();
           if(MB!=null)
           {
            db.ManagerBuilding.Remove(MB);
            db.SaveChanges();
            }
            return RedirectToAction("SelectBuilding", model);

        }
       //Assign building to manager
       public ActionResult AssignBtoM()
        {
            //TODO: View and manage roles manager table needs to add and remove managers

            var Managers = db.Manager.ToList();
            return View(Managers);
        }
       [HttpGet]
       public ActionResult ManagementBuilding(int BuildingID)
        {



            ViewBag.ClientID = db.Buildings.Where(b => b.ID == BuildingID).FirstOrDefault().Clients.ID;
            ViewBag.BuildingName=  db.Buildings.Where(b => b.ID == BuildingID).FirstOrDefault().BuildingName;
            var ObjMB = new ManagementBuilding();
            //---------------------------------------------------------------------------------------------
            ObjMB.managerVM = db.ManagerBuilding
                .Where(c => c.BuildingID == BuildingID)
                .Select(c => new ManagerVM { FullName = c.Manager.FirstName +" " +c.Manager.LastName,
                                             Username  = c.Manager.AspNetUsers.Email,
                                             Phone = c.Manager.Phone,
                                             ID = c.ManagerID
                }).ToList();
            //manager ends-----------------------------------------------------------------------------------------  
            //ClientID
            var clientID =db.Buildings
                .Where(c => c.ID == BuildingID)
                .FirstOrDefault()
                .Clients.ID;           
            foreach (var item in ObjMB.managerVM)
            {
                item.clientID = clientID;
               
            }
            ObjMB.ClientID = clientID;
            ObjMB.buildingID = BuildingID;
           
           //loading Activemanager
           var Activemanager = db.ActiveManager.Where(c => c.BuildingID == BuildingID)
                                                    .Select(c => new ActiveManagerVM {
                                                                                        Id  = c.Id, 
                                                                                        BuildingID =c.BuildingID,
                                                                                    ManagerID =c.ManagerID}).FirstOrDefault();
            if(Activemanager!=null)
            {
                Activemanager.myManager = db.Manager.Find(Activemanager.ManagerID);
            }

            ViewBag.Activemanager = Activemanager;

            return View(ObjMB);
        }
       [HttpPost]
       public async Task<ActionResult> AddManagerTobuilding(ManagementBuilding model, ManagerVM  model2)
        {

            ViewBag.ClientID = db.Buildings.Where(b => b.ID == model.buildingID).FirstOrDefault().Clients.ID;
            ViewBag.BuildingName = db.Buildings.Where(b => b.ID == model.buildingID).FirstOrDefault().BuildingName;

            try
            {
            if (!ModelState.IsValid)
            {
                return View("ManagementBuilding", model);
            }
            ApplicationDbContext context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            PasswordHasher hasher = new PasswordHasher();
            var a = UserManager.FindByEmail(model2.Email);
            if (a != null)
            {
                return View("ManagementBuilding", model);
            }
            ApplicationUser AppUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = model2.Email,
                UserName = model2.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = model2.Phone,
                LockoutEnabled = false,
                LockoutEndDateUtc= DateTime.Now.AddDays(365),
                AccessFailedCount = 0,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(model2.Password)
            };
            string[] FullName = model2.FullName.Split(new string[] { " " }, StringSplitOptions.None);
            Manager mgr = new Manager()
            {
                ID = AppUser.Id,
                FirstName = FullName[0].ToString(),
                LastName = FullName[1].ToString(),
                Phone = model2.Phone,
                ClientID = model2.clientID
            };
            db.Manager.Add(mgr);
            context.Users.Add(AppUser);

            await context.SaveChangesAsync();
            await db.SaveChangesAsync();

            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!RoleManager.RoleExists("Manager"))
            {var roleresult = RoleManager.Create(new IdentityRole("Manager"));}
            var Result = UserManager.AddToRole(AppUser.Id, "Manager");

            ManagerBuilding ObjManagerBuilding = new ManagerBuilding()
            {
                 BuildingID = model2.BuildingID,
                  ManagerID = mgr.ID ,
                   UserID =mgr.ID
            };

            db.ManagerBuilding.Add(ObjManagerBuilding);
            await db.SaveChangesAsync();
         

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("ManagementBuilding", new { BuildingID=model2.BuildingID});
        }        
       [HttpGet]
       public ActionResult RemoveManager(int BuildingID, int ClientID, string ManagerID)
        {
            ViewBag.ClientID = db.Buildings.Where(b => b.ID == BuildingID).FirstOrDefault().Clients.ID;
            ViewBag.BuildingName = db.Buildings.Where(b => b.ID == BuildingID).FirstOrDefault().BuildingName;
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            Manager Obj = db.Manager.Find(ManagerID);
            AspNetUsers user = db.AspNetUsers.Find(ManagerID);
            ManagerBuilding managerb = db.ManagerBuilding.Where(c => c.ManagerID == ManagerID).FirstOrDefault();
           
            
            UserManager.RemoveFromRole(ManagerID, "Manager");//from role Manager
            context.SaveChanges();

            db.ManagerBuilding.Remove(managerb);// mapping row building and user
            db.SaveChanges(); 
               
                 db.Manager.Remove(Obj); //manager
                 db.SaveChanges();

                 db.AspNetUsers.Remove(user);//remove user
                db.SaveChanges();

                return RedirectToAction("ManagementBuilding", new { BuildingID = BuildingID });
        }
       public ActionResult ActivateManager(string managerid, int buildingid)
        {

            ViewBag.ClientID = db.Buildings.Where(b => b.ID == buildingid).FirstOrDefault().Clients.ID;
            ViewBag.BuildingName = db.Buildings.Where(b => b.ID == buildingid).FirstOrDefault().BuildingName;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ManagementBuilding", new { BuildingID = buildingid });
            }


            var deleteanyentry = db.ActiveManager.Where(c => c.BuildingID == buildingid).ToList();
            

            ActiveManager ObjActiveManager = new ActiveManager(){
               BuildingID = buildingid,
               ManagerID = managerid
            };

            if(db.ActiveManager.RemoveRange(deleteanyentry)!=null)
            {
                db.ActiveManager.RemoveRange(deleteanyentry);
            }
            db.ActiveManager.Add(ObjActiveManager);
            db.SaveChanges();


            return RedirectToAction("ManagementBuilding", new { BuildingID = buildingid });
        }
       public ActionResult DeactivateManager(string ManagerID, int BuildingID)
        {

            ActiveManager Obj = db.ActiveManager.Where(c=>c.ManagerID==ManagerID).FirstOrDefault();
            db.ActiveManager.Remove(Obj);
            db.SaveChanges();
            return RedirectToAction("ManagementBuilding", new { BuildingID = BuildingID });
        }
       public ActionResult ManageUsersProfile(int BuildingID)
        {
            //todo: create users, assign level of privilage
            ManageUsersProfileVM mupvm = new ManageUsersProfileVM();
            mupvm.BuildingID = BuildingID;
            ViewBag.BuildingUsers = mupvm.LoadBuildingUsers(BuildingID);
            ViewBag.BuildingInfo = db.Buildings.Find(BuildingID);
           return View(mupvm);
        }

        /// <summary>
        /// This Method handles Creating Building Staff and inserting all given roles. 
        /// </summary>
        /// <param name="BuildingID"></param>
        /// <param name="model_User"></param>
        /// <param name="model_permissions"></param>
        /// <param name="model4"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
       public JsonResult AddUser(int BuildingID, ManagerVM model_User, PermissionBase Permission, ManageUsersProfileVM model4)
        {
            ManageUsersProfileVM Obj = new ManageUsersProfileVM();
            model4.managerVM = model_User;
         
            Obj.managerVM = model_User;
            var UserID = Obj.InsertUser(Obj);  //Create a user to the mail aspNetUser table.         
            var RoleNames = Obj.ConvertToRoleNames(Permission);//Pass all permissions and return a list type string of accepted roles
          var returnUserID = Obj.AddBuildingUser(model_User, UserID);// Insert building staff.
            foreach (var item in RoleNames)// loop throught the list of roles
            {
                Obj.InserUserPermission(item, returnUserID); // Insert one permission at a time. 
            }


            var mydata = Json("");
            return new JsonResult {Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public async Task<ActionResult> EditBUPermissions(int BuildingID, PermissionBase permission, ManageUsersProfileVM model, string UserID)
        {
            
            ManageUsersProfileVM ObjBU = new ManageUsersProfileVM();
           

            var RoleNames = ObjBU.ConvertToRoleNames(permission);
            await ObjBU.EditbuildingUserPermission(RoleNames, UserID);
            return RedirectToAction("ManageUsersProfile", new { BuildingID = BuildingID });
        }
        public ActionResult Loadingbuildinguserpermissions(string BuildingIDUserID)
        {
            ManageUsersProfileVM Objbu = new ManageUsersProfileVM();
           var BuildingUserpermissions = Objbu.LoadBuildingUserPermission(BuildingIDUserID);
           return new JsonResult { Data = BuildingUserpermissions, JsonRequestBehavior = JsonRequestBehavior.AllowGet };   
           
        }
        //public ActionResult EditBuildingUserPermission(int BuildingID)
        //{
        //    ManageUsersProfileVM Objbu = new ManageUsersProfileVM();
        //    Objbu.LoadBuildingUsers(BuildingID);
        //}
      
        public RoleManager<IdentityRole> RoleManager { get; set; }
        
       
    }
}
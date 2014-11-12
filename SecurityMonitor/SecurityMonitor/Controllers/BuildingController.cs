using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models;
using SecurityMonitor.Models.EntityFrameworkFL;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;


namespace SecurityMonitor.Controllers
{
    public class BuildingController : Controller
    {
        PointersecurityEntities1 db = new PointersecurityEntities1();


        public async Task<ActionResult> ClientIndex()
        {
            var clients = await db.Clients
              
               .Select(c => new ClientsVM
               {
                   ID = c.ID,
                   ClientName = c.ClientName,
                   BuildingCount = (int)c.BuildingCount
               }).ToListAsync();
            return View(clients);
        }
        
        [HttpGet]
        public ActionResult AddClient()
        {

           
            return View(); 
        }

        [HttpPost]
        public async Task<ActionResult> AddClient(ClientsVM newClient)
        {
            try 
            {
                if(ModelState.IsValid)
                {
                    var newclient = new Clients
                    {
                        ClientName = newClient.ClientName,
                        BuildingCount = newClient.BuildingCount
                    };

                    db.Clients.Add(newclient);
                   await db.SaveChangesAsync();
            }
         } 
            catch(Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return null;
            }

            return RedirectToAction("ClientIndex");
        }


        public async Task<ActionResult> BuildingIndex(int ClientID) 
        {
            Session["ClientID"] = ClientID;
            var building = await db.Buildings
                .Where(c => c.ClientID == ClientID)
                .Select(c => new BuildingInfoVM
                {
                    BuildingName = c.BuildingName,
                    Address = c.Address,
                    BuildingPhone = c.BuildingPhone,
                    NumberOfApart = (int)c.NumberOfApartment,
                    City = c.City,
                    States = c.State,
                    ZipCode = c.Zipcode,
                    Manager = c.Manager,
                    ClientID = c.ClientID,
                    ID = c.ID
                }).ToListAsync();
            return View(building);
        }
        // GET: Building
        [HttpGet]
        public ActionResult AddBuilding()
        {
           
            var building = new BuildingInfoVM ();



            return View(building);
        }

        [HttpPost]
        public async Task<ActionResult> AddBuilding(BuildingInfoVM apartmentvm)
        {if (ModelState.IsValid)
            {
                //if (User.Identity.IsAuthenticated)
                //{
            if(ModelState.IsValid)
            {
                    var apartment = new Buildings
                    {
                        BuildingName = apartmentvm.BuildingName,
                        Address = apartmentvm.Address,
                        BuildingPhone = apartmentvm.BuildingPhone,
                        NumberOfApartment = apartmentvm.NumberOfApart,
                        City = apartmentvm.City,
                        State = apartmentvm.States,
                        Zipcode = apartmentvm.ZipCode,
                        Manager = apartmentvm.Manager,
                        ClientID = (int)Session["ClientID"]
                    };
                    db.Buildings.Add(apartment);
                    await db.SaveChangesAsync();
                   
                    }  
                //}
            //TODO Exception 
            }



        return RedirectToAction("BuildingIndex", new { ClientID = Session["ClientID"] });
        }

        
        // GET: Building
    [HttpGet]
        public ActionResult AddApartment(int buildingID)
        {


            var apartment = new ApartmentVM();
            apartment.BuildingID = buildingID;

          

            return View(apartment);
        }

        [HttpPost]
        public async Task<ActionResult> AddApartment(ApartmentVM apartmentvm)
        {
            if (ModelState.IsValid)
            { 
                //if (User.Identity.IsAuthenticated)
                //{
                    int BuildingID = (int)Session["BuildingID"];
                    {
                        var apartment = new Apartment
                          {
                              ApartmentNumber = apartmentvm.ApartmentNumber,
                              BuildingID = Convert.ToInt32(apartmentvm.BuildingID),
                              FloorNumber = apartmentvm.FloorNumber
                          };
                        db.Apartment.Add(apartment);
                        await db.SaveChangesAsync();

                        //======================insert Add Building Activity================
                        //var UserID = User.Identity.GetUserId();// gets logged user ID
                        //AspNetUsers myUser = await db.AspNetUsers.FirstOrDefaultAsync(c => c.Id == UserID);select from db where logged use match
                        //var newActivity = new UserActivityLog
                        //{
                        //    BuildingID = BuildingID,
                        //    UserID = User.Identity.GetUserId(),
                        //    DateOfEvent = DateTime.Now,
                        //    FunctionPerformed = "Added apartment",
                        //    Message = "Apartment # " + apartmentvm.ApartmentNumber + " was added by " + myUser.UserName
                        //};
                        //db.UserActivityLog.Add(newActivity);
                        //await db.SaveChangesAsync();
                    }
            }
                //}
           return RedirectToAction("BuildingProfile", "Building");
        }


        //===================== Apartments CSV Import=======================
        public async Task<ActionResult> ProcessCsv(EventItem[] model)
        {
            int BuildingID = (int)Session["BuildingID"];

            if (ModelState.IsValid)

                //if(User.Identity.IsAuthenticated)
                {
                    {
                        foreach (var item in model)
                        {
                            var apartment = new Apartment
                             {
                                 ApartmentNumber = item.AparmentNumber,
                                 BuildingID = BuildingID,
                                 FloorNumber = item.Floor
                             };
                            db.Apartment.Add(apartment);
                            await db.SaveChangesAsync();

                            //======================insert Add Building Activity================
                            //var UserID = User.Identity.GetUserId();// gets logged user ID
                           // AspNetUsers myUser =  await db.AspNetUsers.FirstOrDefaultAsync(c => c.Id == UserID); //select from db where logged use match
                           // var newActivity = new UserActivityLog
                           //{
                           //    BuildingID = BuildingID,
                           //    UserID = User.Identity.GetUserId(),
                           //    DateOfEvent = DateTime.Now,
                           //    FunctionPerformed = "Added apartment",
                           //    Message = "Apartment # "+ item.AparmentNumber +" was added by " + myUser.UserName
                           //};
                           // db.UserActivityLog.Add(newActivity);
                           //await db.SaveChangesAsync();
                        }
                    }
                }
            
            return RedirectToAction("BuildingProfile", "Building");
        }

        public ViewResult Show()
        {
            return View();
        }

        //=============Building Profile =====================
        [HttpGet]
        public async Task<ActionResult> BuildingProfile(int? page, string searchBy, string search, int? BuildingID)
        {


            Session.Timeout = 20;
            
            if (BuildingID != null)
            {
                Session["BuildingID"] = BuildingID;
            }
            else
            {
                BuildingID = (int)Session["BuildingID"];
            }
            var buildinginfo = await db.Buildings
                  .Where(c => c.ID == BuildingID)
                  .Select(c => new BuildingInfoVM
                  {
                      ID = c.ID,
                      BuildingName = c.BuildingName,
                      BuildingPhone = c.BuildingPhone,
                      Address = c.Address,
                      City = c.City,
                      ZipCode = c.Zipcode,
                      Manager = c.Manager,
                      NumberOfApart = (int)c.NumberOfApartment,
                      States = c.State
                  }).FirstAsync();
            Session["Building"] = buildinginfo;
            ViewBag.buildingInfo = buildinginfo;


            //=================building profile appartmentlist==========================================
            if (page == null && searchBy != null && search != null)
            {
                ViewBag.searchBy = searchBy;
                ViewBag.search = search;
            }
            if (page != null && searchBy != null && search != null)
            {
                ViewBag.searchBy = searchBy;
                ViewBag.search = search;
            }
            if (Request.HttpMethod != "GET")
            {
                page = 1; // after post reset page to 1

            }
            int pageSize = 96;
            int pageNumber = (page ?? 1);

            if (searchBy == "ApartmentNumber")
            {
                //executes when there is a search
                var apartmentlist = await db.Apartment
               .Where(c => c.BuildingID == BuildingID && c.ApartmentNumber.Contains(search))
               .Select(c => new ApartmentVM
               {
                   ApartmentNumber = c.ApartmentNumber

               }).ToListAsync();
                ViewBag.apartmentlist = apartmentlist.ToPagedList(pageNumber, pageSize);
            }
            else
            {
                //executes when there is no search
                var apartmentlist = await db.Apartment
                    .Where(c => c.BuildingID == BuildingID)
                    .Select(c => new ApartmentVM
                    {
                        ApartmentNumber = c.ApartmentNumber,
                        ID = c.ID

                    }).ToListAsync();
                ViewBag.apartmentlist = apartmentlist.ToPagedList(pageNumber, pageSize);

            }

            

            return View( );
        }

        //=====================Activity Log=======================
        [HttpGet]
        public ActionResult ActivityPartial(int? page, string searchBy, string search)
        {

            //var currentUserID = User.Identity.GetUserId();
            if (Request.HttpMethod != "GET")
            {
                page = 1; // after post reset page to 1

            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            //=================user activity===============================
            var myUserActivitiesLogVM = new UserActivityLogVM();
            //if (User.Identity.IsAuthenticated != false)
            //{
                //string myUserID = User.Identity.GetUserId().ToString();
                if (page == null && searchBy != null && search != null)
                {
                    ViewBag.searchBy = searchBy;
                    ViewBag.search = search;
                }
                if (page != null && searchBy != null && search != null)
                {
                    ViewBag.searchBy = searchBy;
                    ViewBag.search = search;
                }

                int BuildingID = (int)Session["BuildingID"];
                //=============Search=================
                if (searchBy == "Function")
                {
                    myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                               .Where(UAL => UAL.BuildingID == BuildingID && UAL.FunctionPerformed.Contains(search))
                               .Select(UAL => new ActivityLog
                               {
                                   UserID = UAL.UserID,
                                   ID = UAL.ID,
                                   DateCreated = UAL.DateOfEvent,
                                   FunctionPerformed = UAL.FunctionPerformed,
                                   Message = UAL.Message
                               }).ToList();

                }

                else if (searchBy == "Date")
                {
                    DateTime myvar = new DateTime();
                    if (DateTime.TryParse(search, out myvar))
                    {
                        string actualdate = search.Substring(0, 10);
                        DateTime theTime = Convert.ToDateTime(actualdate);
                        myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                                   .Where(UAL => UAL.BuildingID == BuildingID && UAL.DateOfEvent == theTime)
                                   .Select(UAL => new ActivityLog
                                   {
                                       UserID = UAL.UserID,
                                       ID = UAL.ID,
                                       DateCreated = UAL.DateOfEvent,
                                       FunctionPerformed = UAL.FunctionPerformed,
                                       Message = UAL.Message
                                   }).ToList();
                    }
                    else
                    {
                        ViewBag.ItisNotaDay = search;

                        myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                             .Where(UAL => UAL.BuildingID == BuildingID)
                             .Select(UAL => new ActivityLog
                             {
                                 UserID = UAL.UserID,
                                 ID = UAL.ID,
                                 DateCreated = UAL.DateOfEvent,
                                 FunctionPerformed = UAL.FunctionPerformed,
                                 Message = UAL.Message,

                             }).ToList();
                    }
                }
                else
                {
                   
                    myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                              .Where(UAL => UAL.BuildingID == BuildingID)
                              .Select(UAL => new ActivityLog
                              {
                                  UserID = UAL.UserID,
                                  ID = UAL.ID,
                                  DateCreated = UAL.DateOfEvent,
                                  FunctionPerformed = UAL.FunctionPerformed,
                                  Message = UAL.Message,

                              }).ToList();
                }
               
                ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM;
            //}
              return PartialView(myUserActivitiesLogVM.UserActivites.ToPagedList(pageNumber, pageSize));
        }


        //====================apartmenprofile ==========-==========

        [HttpGet]
        public async Task<ActionResult> ApartmentProfile(int? ApartmentID) 
        {


            var buildinginfo = await db.Buildings
                .Join(db.Apartment,
                b => b.ID,
                c => c.BuildingID,
                (b, c) => new BuildingInfoVM
                {
                    ID = c.ID,
                    BuildingName = b.BuildingName,
                    BuildingPhone = b.BuildingPhone,
                    Address = b.Address,
                    City = b.City,
                    ZipCode = b.Zipcode,
                    Manager = b.Manager,
                    NumberOfApart = (int)b.NumberOfApartment,
                    States = b.State,
                    AptID = c.ID
                })
                .Where(cb => cb.AptID == ApartmentID)
                .FirstAsync();

          Session["Building"] = buildinginfo;
            
            var apartmentinfo = new ApartmentVM();
            var apartmentprofile  = await db.Apartment
                                            .Where( a=>a.ID == ApartmentID)                                          
                                            .Select(c=> new ApartmentVM{   ApartmentNumber= c.ApartmentNumber,
                                                                         FloorNumber = c.FloorNumber,
                                                                         BuildingID = c.BuildingID, ID = c.ID}).ToListAsync();
            
            var tenant = await db.Tenant
                .Where(t => t.aptID == ApartmentID).ToListAsync();

            ViewBag.tenant = tenant;

            return View(apartmentprofile);
        }

        //===================Adding Tenant to apartment GET ==============
        [HttpGet]
        public ActionResult AddingTenant(int? apartmentID)
        {
            if (apartmentID != null)
            {
                var newtenant = new TenantVM();
                newtenant.aptID = (int)apartmentID;
                return View(newtenant);
            }
            return View("page doesn't meet the required elements");
        }

        //============= Adding Tenant to apartment Post ==============
        [HttpPost]
        public async Task<ActionResult> AddingTenant(TenantVM newTenant)
        {
            try { 
            
           
            if (ModelState.IsValid)
            {
                var newtenant = new Tenant
                {
                    FirstName = newTenant.FirstName,
                    LastName = newTenant.LastName,
                    Phone = newTenant.Phone,
                    Created = DateTime.Now,
                    aptID = newTenant.aptID,
                    Username = newTenant.Username
                };
                db.Tenant.Add(newtenant);
                await db.SaveChangesAsync();
                return RedirectToAction("ApartmentProfile", new { ApartmentID = newTenant.aptID });

            }
            }
                catch(Exception e){

                    ViewBag.Message = e.Message;
                
            }
            return View();
        }

        //===================DeleteTenant=============

        [HttpGet]
        public async Task<ActionResult> DeleteTenant(int? TenantID)
        {
            if (TenantID != null)
            {
                var tenant = await db.Tenant.FindAsync(TenantID);

                if (tenant == null)
                {
                    return HttpNotFound();
                }
                TenantVM tenantVM = new TenantVM
                {
                     FirstName = tenant.FirstName,
                     LastName = tenant.LastName,
                     Phone = tenant.Phone,
                     Username = tenant.Username,
                     aptID = (int)tenant.aptID,
                     ID = tenant.ID,
                     created = tenant.Created

                
                };
                ViewBag.aptID = tenant.aptID;
                ViewBag.tenantID = tenant.ID;
                return View(tenantVM);
             
            }
            
            return View();
        }
        //======================Delete Tenant POST=======================
        [HttpPost, ActionName("DeleteTenant")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTenant(int? removeTenantID, int? ApartmentID)
        {
            if (ModelState.IsValid)
            {
                var RemovethisTenant = await db.Tenant.FindAsync(removeTenantID);
                
                db.Tenant.Remove(RemovethisTenant);
                await db.SaveChangesAsync();
                return RedirectToAction("ApartmentProfile", new { ApartmentID = ApartmentID });
            }
            return View();
        }

        //======================TenantRequest======================
        [HttpGet]
        public ActionResult TenantRequest(int? tenantID) 
        {

            if (tenantID != null)
            {
                var tenantRequest = new Requests();

            
                tenantRequest.TenantID = (int)tenantID;

                List<SelectListItem> reqtype = new List<SelectListItem>();
                var myitems = db.ReqType.ToList();

                foreach (var item in myitems)
                {
                    reqtype.Add(new SelectListItem { Text = item.ReqType1, Value = item.ReqType1 });

                }
             



                ViewBag.ReqType = reqtype;
    



                return View(tenantRequest);
            }

            return RedirectToAction("Index", "DashBoard");
        }

        [HttpPost]
        public ActionResult TenantRequest(Requests model)
        {

            var tenant = db.Tenant.Find(model.TenantID);
                       
            if (ModelState.IsValid)
            {
                db.Requests.Add(model);
                db.SaveChanges();
                return RedirectToAction("ApartmentProfile", new { ApartmentID = tenant.aptID });
            }
            return View();
        }


        //======================Tenant Request chart======================
        public JsonResult getRequestsType(int? TenantID)
        {

            var Tomorrow = DateTime.Today.AddDays(1);
            var Today = DateTime.Today.Date;
            //Request From Today
            var todayRequests = db.Requests
                 .Where(c => c.TenantID ==(int)TenantID )
                 .GroupBy(c=> c.RequestType)
                 .Select(c => new ReqTypeCount { key = c.Key, AccessControl = c.Count() }).ToList();



            var dayoftheweek = DateTime.Today.AddDays(3).DayOfWeek;

            return new JsonResult { Data = todayRequests, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        

    }
}

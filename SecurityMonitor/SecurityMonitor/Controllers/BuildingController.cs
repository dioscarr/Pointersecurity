﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models;
using PointerSecurityAzure;

using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;


//TO DO: 

//12-19-2014
//this needs to be handle on while adding buildin. 
// cound should increase when adding building and decrease when deleting building


namespace SecurityMonitor.Controllers
{
    public class BuildingController : Controller
    {
        //DB context
        pointersecurityEntities db = new pointersecurityEntities();

        
        //shared_layout
        public ActionResult shared_layoutAllOpenRequests()
        {
            var Tomorrow = DateTime.Today.AddDays(1);
        
            var TotalReq = db.Requests.Where(c=>c.FromDate < Tomorrow && c.ToDate >= DateTime.Today).Count();

            
            return new JsonResult { Data = TotalReq, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //shared_layout
        public ActionResult shared_layoutnext7Requests()
        {
            var Next7 = DateTime.Today.AddDays(7);
            var Tomorrow = DateTime.Today.AddDays(1);
            var Today = DateTime.Today;

            var TotalReq = db.Requests.Where(c => c.FromDate >= Today && c.FromDate <= Next7 || c.FromDate < Today && c.ToDate >= Next7).Count();


            return new JsonResult { Data = TotalReq, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }      
        
        
        //client index
    [HttpGet]
        public ActionResult ClientIndex()
        {
            //this is for the add client partial view
            ViewBag.ClientModel = new ClientsVM();
            
        
        var ObjClients = new ClientsVM();
        ObjClients.ListOfClients = db.Clients
               .Select(c => new ClientsVM
               {
                   ID = c.ID,
                   ClientName = c.ClientName,
                   BuildingCount = (int)c.BuildingCount,
                   Address = c.Address,
                    Phone =c.Phone,
                     city=c.City,
                      State = c.State,
                      zipcode = c.ZipCode,
                       Fax = c.Fax,
                       Email = c.Email
                    
                  
               }).Take(10).ToList();

        ObjClients.States = db.States.Select(c => new State { value = c.State, myState = c.State }).ToList();

        foreach (var item in ObjClients.ListOfClients)
            {
                if (item.BuildingCount <=0)
                {
                    item.BuildingCount = 0;
                }
            };
        return View(ObjClients);
        }

        [HttpPost]
        public ActionResult ClientIndex(string ClientName)
        {

            if (ModelState.IsValid)
            {
                if (ClientName != "")
                {
                    var Client = new Clients
                    {
                        ClientName = ClientName,
                        BuildingCount = 10,

                        //TO DO: update Clients table with matching fields from ClientsVM...
                    };
                    db.Clients.Add(Client);
                    db.SaveChanges();
                }
            }


            var clients = db.Clients

              .Select(c => new ClientsVM
              {
                  ID = c.ID,
                  ClientName = c.ClientName,
                  BuildingCount = (int)c.BuildingCount
              }).Take(10).ToList();
            return View(clients);

        }

        public async Task<ActionResult> LoadingClients(int? skip)
        {
            if (skip !=null)
            {
            if (Request.IsAjaxRequest())
            {

                var clientsList=await db.Clients.OrderByDescending(c=>c.ClientName).Skip((int)skip).Take(10).Select(c=> new{ClientName=c.ClientName, id = c.ID}).ToListAsync();
               return  new JsonResult { Data = clientsList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               
             
            }
            }
            
            return RedirectToAction("clientsIndex");
        }


        //[HttpGet]
        //public ActionResult AddClient()
        //{


        //    return View();
        //}

        [HttpPost]
        public ActionResult AddClient(ClientsVM model, int? buildingcount, int? ContactID)
        {




            try 
            {

                if (ModelState.IsValid)
                {

                    if (buildingcount == null)
                    {
                        //TO DO: this needs to be handle on while adding buildin. 
                        // cound should increase when adding building and decrease when deleting building
                        buildingcount = 0;
                    }
                    var newclient = new Clients
                    {
                        ClientName = model.ClientName,
                        BuildingCount = (int)buildingcount,
                        Address = model.Address,
                        City = model.city,
                        State = model.State,
                        ZipCode = model.zipcode,
                        Fax = model.Fax,
                        Phone = model.Phone,
                        Email = model.Email
                    };

                    db.Clients.Add(newclient);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("clientIndex");
                    
                }
         } 
            catch(Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return null;
            }

            return RedirectToAction("ClientIndex");
        }
        //client Edit
        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var client = db.Clients.Find(id);
            return View(client);
        }

           //client Edit Post
        [HttpPost]
        public ActionResult EditClient(Clients model)
        {

            if (ModelState.IsValid)
            {
                //var client = new Clients
                //{
                //     ID =model.ID,
                //     ClientName = model.ClientName,
                //     BuildingCount = model.BuildingCount
                //};
                model.BuildingCount = 0;
                db.Clients.Attach(model);
                var Entry = db.Entry(model);
                Entry.Property(c => c.ClientName).IsModified = true;
                Entry.Property(c => c.Address).IsModified = true;
                Entry.Property(c => c.City).IsModified = true;
                Entry.Property(c => c.State).IsModified = true;
                Entry.Property(c => c.Phone).IsModified = true;
                Entry.Property(c => c.Fax).IsModified = true;
                Entry.Property(c => c.Email ).IsModified = true;
                Entry.Property(c => c.BuildingCount).IsModified = true;

                db.SaveChanges();
            }
            return RedirectToAction("ClientIndex");
        }

        //Client Delete
        [HttpGet]
        public async Task<ActionResult> deleteClient(int? id)
        {
            var client = await db.Clients.FirstOrDefaultAsync(c=>c.ID==id);

            return View(client);
        }
        //Client Delete
        [HttpPost]
        public async Task<ActionResult> deleteClient(Clients model)
        {

            if (ModelState.IsValid)
            {
                var client = await db.Clients.FindAsync(model.ID);
                db.Clients.Remove(client);
                await db.SaveChangesAsync();
            }


              
            return RedirectToAction("ClientIndex");
        }
    [HttpGet]
        public async Task<ActionResult> BuildingIndex(int ClientID) 
        {
            ViewBag.ClientID = ClientID;
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
                    ClientID = (int)c.ClientID,
                    ID = c.ID
                }).ToListAsync();
            return View(building);
        }
        // GET: Building
        [HttpGet]
        public ActionResult AddBuilding(int? ClientID)
        {
           
            var building = new BuildingInfoVM();
            building.StatesList =db.States.Select(c=> new State{myState =c.State, value=c.State }).ToList();
            if (ClientID != null)
            {
                ViewBag.ClientID = ClientID;
                building.ClientID = (int)ClientID;
            }

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
                        ClientID = apartmentvm.ClientID
                    };
                    db.Buildings.Add(apartment);
                    await db.SaveChangesAsync();
                   
                    }  
                //}
            //TODO Exception 
            }



        return RedirectToAction("BuildingIndex", new { ClientID = apartmentvm.ClientID });
        }

        //Delete building ##########################################################################################################
        //TO DO: no views define yet
        [HttpGet]
        public ActionResult ErrorpageMessage(string errorMessage, int ClientID)
        {

            var objerrormessage = new ErrorMessageVM();

            objerrormessage.ErrorMessagestring = errorMessage;
            objerrormessage.ClientID = ClientID;

            return View(objerrormessage);
        
        
        }
        [HttpGet]
        public ActionResult BuildingDelete(int id, int? ClientID)
        {
          //check if building has apartments asigned or null
           var Apartments =db.Apartment.Where(c => c.BuildingID == id).Select(c=> new {}).ToList();
           if (Apartments.Count !=0)
            {

                return RedirectToAction("ErrorpageMessage", new { errorMessage = "Cannot be delete since it has apartments.", ClientID = ClientID });
            
            }


            var building = db.Buildings.Find(id);
            building.ClientID=ClientID;
            return View(building);
        
        }
        [HttpPost]
        public async Task<ActionResult> BuildingDelete(Buildings model, int BuildingID, int? ClientID)
        {
            if (ModelState.IsValid)
            {
                var mynewbuilding = new Buildings
                {
                     BuildingName = model.BuildingName,
                      Address = model.Address,
                       City =model.City,
                     State = model.City,
                     Zipcode = model.Zipcode,
                     ClientID = ClientID,
                     BuildingPhone = model.BuildingPhone,
                     NumberOfApartment = model.NumberOfApartment,
                     ID = model.ID


                };


                var Entry = db.Entry(mynewbuilding);
                if (Entry.State == EntityState.Detached) 
                { 
                    db.Buildings.Attach(mynewbuilding);
                }
               
                db.Buildings.Remove(mynewbuilding);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("BuildingIndex", new { ClientID = ClientID });
        }

        public ActionResult BuildingEdit(int id)
        {
            var building = db.Buildings.Find(id);

            return View(building);
        
        }
        [HttpPost]
        public async Task<ActionResult> BuildingEdit(Buildings model, int id)
        {
            if (ModelState.IsValid)
            {
                db.Buildings.Attach(model);
                var Entry = db.Entry(model);
                Entry.Property(c => c.BuildingName).IsModified = true;
                Entry.Property(c => c.Address).IsModified = true;
                Entry.Property(c => c.City).IsModified = true;
                Entry.Property(c => c.State).IsModified = true;
                Entry.Property(c => c.Manager).IsModified = true;
                Entry.Property(c => c.Zipcode).IsModified = true;
                Entry.Property(c => c.BuildingPhone).IsModified = true;
                await db.SaveChangesAsync();
            
            }

            return RedirectToAction("buildingIndex", new {ClientID = model.ClientID });
        
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
        public async Task<ActionResult> AddApartment(ApartmentVM apartmentvm, int buildingID)
        {
            
            
            if (ModelState.IsValid)
            { 
                //if (User.Identity.IsAuthenticated)
                //{
                 
                        var apartment = new Apartment
                          {
                              ApartmentNumber = apartmentvm.ApartmentNumber,
                              BuildingID =buildingID,
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
                //}
            return RedirectToAction("BuildingProfile", "Building", new { BuildingID = buildingID});
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
                            var UserID = User.Identity.GetUserId();// gets logged user ID
                            AspNetUsers myUser =  await db.AspNetUsers.FirstOrDefaultAsync(c => c.Id == UserID);// select from db where logged use match
                            var newActivity = new UserActivityLog
                           {
                               BuildingID = BuildingID,
                               UserID = "84a4092e-0bcc-4e4f-b007-c87a99eccae3",
                               DateOfEvent = DateTime.Now,
                               FunctionPerformed = "Added apartment",
                               Message = "Apartment # "+ item.AparmentNumber +" was added by " + myUser.UserName
                           };
                            db.UserActivityLog.Add(newActivity);
                           await db.SaveChangesAsync();
                        }
                    }
                }
            
            return RedirectToAction("BuildingProfile", "Building");
        }
        //remove all apartment from the building========================================
        [HttpGet]
        public async Task<ActionResult> BuildingApartmentDeleteAll(int BuildingID)
        {
            var t = await db.Tenant.Where(c => c.Apartment.Buildings.ID == BuildingID).ToListAsync();
            var r = await db.Requests.Where(c => c.Tenant.Apartment.Buildings.ID == BuildingID).ToListAsync();
            ApartmentdeleteAll APtDelAll = new ApartmentdeleteAll() {BuildingID = BuildingID};
            APtDelAll.TenantList = t;
            APtDelAll.RequestList = r;
            return View(APtDelAll);
        }
        [HttpPost]
        public async Task<ActionResult> BuildingApartmentDeleteAll(int BuildingID, string x)
        {
            var AllApartments = await db.Apartment.Where(a => a.BuildingID == BuildingID).ToListAsync();
            db.Apartment.RemoveRange(AllApartments);
            await db.SaveChangesAsync();
            return RedirectToAction("BuildingProfile", new { BuildingID = BuildingID });
        }
        //============================================================
        [HttpGet]
        public ActionResult BuildingTenantDeleteAll(int BuildingID) 
        {
            ViewBag.BuildingID = BuildingID;

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> BuildingTenantDeleteAll(int BuildingID, string x)
        {
            List<Tenant> tenantList =await  db.Tenant.Where(c => c.Apartment.Buildings.ID == BuildingID).ToListAsync();
            db.Tenant.RemoveRange(tenantList);
            await db.SaveChangesAsync();
            return RedirectToAction("ApartmentDeleteAll", new {BuildingID =BuildingID} );
        }

        [HttpGet]
        public ActionResult BuildingAllRequestDelete(int BuildingID)
        {
            ViewBag.BuildingID = BuildingID;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> BuildingAllRequestDelete(int BuildingID, string x)
        {
            var RequesList = await db.Requests.Where(r => r.Tenant.Apartment.Buildings.ID == BuildingID).ToListAsync();
            db.Requests.RemoveRange(RequesList);
            await db.SaveChangesAsync();
            return RedirectToAction("ApartmentDeleteAll", new {BuildingID =BuildingID} );
        }

        //delete apartment
        [HttpGet]
        public ActionResult deleteApartment(int apartmentID) 
        {
            var b = db.Apartment.Find(apartmentID);
           
            
           
            return View(b);
        }
        [HttpPost]
        public ActionResult deleteApartment(Apartment model, int id)
        {

            if (ModelState.IsValid) {
                var b = db.Apartment.Find(id);
                db.Apartment.Remove(b);
                db.SaveChanges();
            }
            return RedirectToAction("BuildingProfile", new { BuildingID = model.BuildingID });
        
        }


        public ViewResult Show()
        {
            return View();
        }

        //=============Building Profile =====================
        [HttpGet]
        public async Task<ActionResult> BuildingProfile(int? page, string search, int? BuildingID)
        {

            ViewBag.Manager = db.ManagerBuilding
               .Where(c => c.BuildingID == BuildingID)
               .Select(c => new ManagerVM
               {
                   FullName = c.Manager.FirstName + " " + c.Manager.LastName,
                   Username = c.Manager.AspNetUsers.Email,
                   Phone = c.Manager.Phone
               }).FirstOrDefault();

            Session.Timeout = 20;

            ViewBag.BuildingID = BuildingID;

            //Module
            ViewBag.Module = db.Module.Where(m => m.BuildingID == BuildingID).ToList();
            ViewBag.ModuleCount = db.Module.Where(model => model.BuildingID == BuildingID).Count();

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
            if (page == null  && search != null)
            {
               
                ViewBag.search = search;
            }
            if (page != null && search != null)
            {
               
                ViewBag.search = search;
            }
            if (Request.HttpMethod != "GET")
            {
                page = 1; // after post reset page to 1

            }
            int pageSize = 96;
            int pageNumber = (page ?? 1);

            if (search !=null)
            {
                //executes when there is a search
                var apartmentlist = await db.Apartment
               .Where(c => c.BuildingID == BuildingID && c.ApartmentNumber.Contains(search))
               .Select(c => new ApartmentVM
               {
                   ID = c.ID,
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

            //loading Activemanager
            var Activemanager = db.ActiveManager.Where(c => c.BuildingID == BuildingID)
                                                     .Select(c => new ActiveManagerVM
                                                     {
                                                         Id = c.Id,
                                                         BuildingID = c.BuildingID,
                                                         ManagerID = c.ManagerID
                                                     }).FirstOrDefault();
            if (Activemanager != null)
            {
                Activemanager.myManager = db.Manager.Find(Activemanager.ManagerID);
            }

            ViewBag.Activemanager = Activemanager;

           

            

            return View( );
        }

        //=================BuildingRequestsHistory==========================
        [HttpGet]
        public ActionResult BuildingRequestHistoryIndex(int BuildingID) 
        {
            var BR = db.Requests.Where(r => r.Tenant.Apartment.BuildingID ==BuildingID).ToList();
            return View(BR); //done
        }
        [HttpGet]
        public ActionResult BuildingRequestHistoryEdit(int RequestID) 
        {
            Requests request = db.Requests.Find(RequestID);
            var reqType = db.ReqType.Select(c => new SelectListItem { Text= c.ReqType1, Value= c.ReqType1 }).ToList();
            //dropdownlist
            ViewBag.RequestTypeEdit = reqType;
            return View(request); 
        }
        [HttpPost]
        public ActionResult BuildingRequestHistoryEdit(int RequestID, int BuildingID)
        {
            Requests request = db.Requests.Find(RequestID);
            db.Requests.Attach(request);
            var Entry = db.Entry(request);
            Entry.Property(c => c.Description).IsModified = true;
            Entry.Property(c => c.PIN).IsModified = true;
            Entry.Property(c => c.RequestType).IsModified = true;
            Entry.Property(c => c.ToDate).IsModified = true;
            Entry.Property(c => c.FromDate).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("BuildingRequestHistoryIndex", new { BuildingID = BuildingID });
        }
        [HttpGet]
        public ActionResult BuildingRequestHistoryDelete(int RequestID)
        {
            Requests request = db.Requests.Find(RequestID);
            return View(request);
        }
        [HttpPost]
        public ActionResult BuildingRequestHistoryDelete(int RequestID, int BuildingID)
        {
            if (ModelState.IsValid)
            {
                Requests req = db.Requests.Find(RequestID);
                db.Requests.Remove(req);
                db.SaveChanges();
            
            }
            return RedirectToAction("BuildingRequestHistoryIndex", new {BuildingID = BuildingID }); 
        }

        //Contact Book
        [HttpGet]
        public ActionResult ContactBook(int BuildingID)
        {
            List<Tenant> tn = db.Tenant.Where(t => t.Apartment.Buildings.ID == BuildingID).ToList();
            return View(tn);
        }
        //ContactBookCount
        public ActionResult ContactBookCount(int BuildingID)
        {
            var TotalReq = db.Tenant.Where(t => t.Apartment.Buildings.ID == BuildingID).Count();
            return new JsonResult { Data = TotalReq, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }      

        public ActionResult LoadBuildingReq(int BuildingID)
        {
            var TotalReq = db.Requests.Where(b => b.Tenant.Apartment.BuildingID == BuildingID).Count();

            return new JsonResult { Data = TotalReq, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
              
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
        public async Task<ActionResult> ApartmentProfile(int? ApartmentID, int BuildingID) 
        {


            var buildinginfo = await db.Buildings
                .Join(db.Apartment,
                b => b.ID,
                c => c.BuildingID,
                (b, c) => new BuildingInfoVM
                {
                    ID = c.ID,
                    BuildingID = b.ID,
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
                                                                         BuildingID = (int)c.BuildingID, ID = c.ID}).ToListAsync();
            
            var tenant = await db.Tenant
                .Where(t => t.aptID == ApartmentID).ToListAsync();

            ViewBag.tenant = tenant;

            return View(apartmentprofile);
        }

        //===================Adding Tenant to apartment GET ==============
        [HttpGet]
        public ActionResult AddingTenant(int? apartmentID, int BuildingID)
        {
            if (apartmentID != null)
            {
                var newtenant = new TenantVM();
                newtenant.aptID = (int)apartmentID;
                newtenant.BuildingID = BuildingID;
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
                return RedirectToAction("ApartmentProfile", new { ApartmentID = newTenant.aptID, BuildingID= newTenant.BuildingID });

            }
            }
                catch(Exception e){

                    ViewBag.Message = e.Message;
                
            }
            return View();
        }
        //Tenant Edit
        [HttpGet]
        public ActionResult TenantEdit(int TenantID)
        {
            Tenant tn = db.Tenant.Find(TenantID);
            return View(tn);        
        }
        [HttpPost]
        public ActionResult TenantEdit(Tenant model,int ApartmentID, int BuildingID)
        {   //this i use forthe link since model didn't pass Nav properties
           
 
            db.Tenant.Attach(model);
            var Entry = db.Entry(model);
            Entry.Property(c => c.FirstName).IsModified = true;
            Entry.Property(c => c.LastName).IsModified = true;
            Entry.Property(c => c.Phone).IsModified = true;
            Entry.Property(c => c.Username).IsModified = true;
            
            db.SaveChanges();
           
            return RedirectToAction("ApartmentProfile", new { ApartmentID = ApartmentID, BuildingID =BuildingID});
        }
        //===================DeleteTenant=============
        [HttpGet]
        public ActionResult TenantDelete(int TenantID)
        {
            Tenant tn = db.Tenant.Find(TenantID);
            return View(tn);
        }
        //======================Delete Tenant POST=======================
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult TenantDelete(int TenantID, int ApartmentID, int BuildingID)
        {
          
                var tn =  db.Tenant.Find(TenantID);
                
                db.Tenant.Remove(tn);
               db.SaveChanges();
                return RedirectToAction("ApartmentProfile", new { ApartmentID = ApartmentID, BuildingID = BuildingID });
            
           
        }

        //======================TenantRequest======================
        [HttpGet]
        public ActionResult TenantRequest(int? tenantID, int BuildingID ) 
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
                //this is because buildingID is needed.
                ViewBag.RequestBuildingID = BuildingID;
                ViewBag.TenantID = tenantID;
                ViewBag.ReqType = reqtype;

                return View(tenantRequest);
            }

            return RedirectToAction("Index", "DashBoard");
        }

        [HttpPost]
        public ActionResult TenantRequest(Requests model, int BuildingID, int TenantID)
        {
            var tenantApt = db.Tenant.FirstOrDefault(c=>c.ID == TenantID);
                       
            if (ModelState.IsValid)
            {
                db.Requests.Add(model);
                db.SaveChanges();
                return RedirectToAction("ApartmentProfile", new { ApartmentID = tenantApt.aptID, BuildingID = BuildingID });
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


       //History -- requests
        public ActionResult RequestHistoryIndex(int TenantID)
        {
            var requests = db.Requests.Where(r=>r.TenantID==TenantID).ToList();

            return View(requests);
        }

        //deleteRequestHistoryIndex
        [HttpGet]
        public ActionResult RequestdeleteHistory(int RequestID)
        {
            Requests Request = db.Requests.Find(RequestID);
            return View(Request);        
        }
        
        [HttpPost]
        public ActionResult RequestdeleteHistory(int RequestID, int ApartmentID)
        {
             Requests ObjRequest = db.Requests.Find(RequestID);
             var ObjTenantID = ObjRequest.Tenant.ID;
            if (ModelState.IsValid)
            {
               Requests Request = db.Requests.Find(RequestID);
               db.Requests.Remove(Request);
                db.SaveChanges();
            }
            return RedirectToAction("RequestHistoryIndex", new { TenantID = ObjTenantID });
        
        }

        //Tenant Delivary
        public ActionResult TenantDeliveryIndex(int TenantID)
        {
            return View();
        
        }

        //Tenant Messege Center
        public ActionResult TenantMessegeCenterIndex(int TenantID)
        {  
            return View();
        }

    }
}

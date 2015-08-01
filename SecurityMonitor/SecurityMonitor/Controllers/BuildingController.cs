using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models;
using Doormandondemand;
using System.Drawing;
using Root.Reports;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;
using SecurityMonitor.Workes;
using System.Net.Mail;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Text;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;




//TO DO: 

//12-19-2014
//this needs to be handle on while adding buildin. 
// cound should increase when adding building and decrease when deleting building

namespace SecurityMonitor.Controllers
{

    [Authorize(Roles = "Admin")]
    public class BuildingController : Controller
    {
        //DB context
        PointerdbEntities db = new PointerdbEntities();

        
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
        public JsonResult AddClient(Clients model)
        {
            try 
            {

                if (ModelState.IsValid)
                {
                    db.Clients.Add(model);
                    db.SaveChanges();
                }

                var clientList = db.Clients.Select(c => new
                {
                    ClientName = c.ClientName,
                    ClientPhone = c.Phone,
                    ClientFullAddress = c.Address + " " + c.City + " " + c.State + " " + c.ZipCode,
                    ClientEmail = c.Email,
                    ClientFax = c.Fax,
                    ClientAddress = c.Address,
                    ClientCity = c.City,
                    ClientState = c.State,
                    ClientZipcode = c.ZipCode,
                    BUildingID = c.ID
                }).ToList();
                var mydata = Json(clientList);
                return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


              
         } 
            catch(Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return null;
            }

          
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
        public async Task<JsonResult> deleteClient(int ClientID)
        {

            if (ModelState.IsValid)
            {
                Clients model = db.Clients.Find(ClientID);
                var client = await db.Clients.FindAsync(model.ID);
                db.Clients.Remove(client);
                await db.SaveChangesAsync();
            }
            
            var clientList = db.Clients.Select(c => new
             {
                 ClientName = c.ClientName,
                 ClientPhone = c.Phone,
                 ClientFullAddress = c.Address + " " + c.City + " " + c.State + " " + c.ZipCode,
                 ClientEmail = c.Email,
                 ClientFax = c.Fax,
                 ClientAddress = c.Address,
                 ClientCity = c.City,
                 ClientState = c.State,
                 ClientZipcode = c.ZipCode,
                 BUildingID = c.ID

             }).ToList();
            var mydata = Json(clientList);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

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
        public async Task<JsonResult> AddBuilding(Buildings apartmentvm)
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
                        City = apartmentvm.City,
                        State = apartmentvm.State,
                        Zipcode = apartmentvm.Zipcode,
                        Manager = apartmentvm.Manager,
                        ClientID = apartmentvm.ClientID
                    };

                    db.Buildings.Add(apartment);
                    await db.SaveChangesAsync();
                   
                    }  
                //}
            //TODO Exception 
            }
        var CBL = db.Buildings.Where(c => c.Clients.ID == apartmentvm.ClientID).Select(c => new
                {
                    BuildingName = c.BuildingName,
                    BuildingAddress = c.Address + " " + c.City + ", " + c.State + " " + c.Zipcode,
                    Phone = c.BuildingPhone,
                    BuildingID = c.ID

                }).ToList();

                var mydata = Json(CBL);

                return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        [AllowAnonymous]
        public JsonResult BuildingEdit(int id)
        {
            var building = db.Buildings.Where(c=>c.ID == id).Select(c=> new {
            
                BuildingName = c.BuildingName,
                BuildingAddress = c.Address,
                BuildingCity = c.City,
                BuildingState = c.State,
                BuildingZipcode =c.Zipcode,
                BuildingPhone = c.BuildingPhone
            });

            var mydata = Json(building);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> BuildingEdit(Buildings model)
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

            var building = db.Buildings.Where(c => c.ID == model.ID).Select(c => new
            {

                BuildingName = c.BuildingName,
                BuildingAddress = c.Address,
                BuildingCity = c.City,
                BuildingState = c.State,
                BuildingZipcode = c.Zipcode,
                BuildingPhone = c.BuildingPhone
            }).FirstOrDefault();

            var mydata = Json(building);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        
        
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
        public async Task<ActionResult> BuildingProfile(int? page, string search, int BuildingID)
        {

            ManageUsersProfileVM ObjBU = new ManageUsersProfileVM();
            List<BuildingUser> ListBU = ObjBU.LoadBuildingUsers(BuildingID);
            ViewBag.BuildingUsers = ListBU;
            ViewBag.ClientName = db.Buildings.Find(BuildingID).Clients.ClientName;
           
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
                      ClientID = c.Clients.ID,                     
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
        public async Task<ActionResult> ApartmentProfile(int ApartmentID, int BuildingID) 
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
        public ActionResult AddingTenant(int apartmentID, int BuildingID)
        {
                var newtenant = new TenantVM();
                newtenant.aptID = (int)apartmentID;
                newtenant.BuildingID = BuildingID;
                return View(newtenant);
        }

        //============= Adding Tenant to apartment Post ==============
        [HttpPost]
        //[ValidateAntiForgeryToken]
            [AllowAnonymous]
        public async Task<JsonResult> AddingTenant(TenantVM newTenant)
        {
            try { 
            
           
            if (ModelState.IsValid)
            {

                string password ="";
                if (newTenant.GenerateAutomaticPassword == false){ password = newTenant.Password;}
                else{ password = PasswordGenerator.GeneratePassword("8").ToString();}

              
                var UserID = RegisterUsers.InsertUser(newTenant.Username, newTenant.Phone, password);
                var result = RegisterUsers.InserToRole("Tenant", UserID);
                
                var tenantdb = new Tenant
                {
                    ID = UserID,
                    FirstName = newTenant.FirstName,
                    LastName = newTenant.LastName,
                    Phone = newTenant.Phone,
                    Created = DateTime.Now,
                    aptID = newTenant.aptID,
                    Username = newTenant.Username
                };
                db.Tenant.Add(tenantdb);
                
                await db.SaveChangesAsync();         
                
                if(newTenant.EmailNotification==true)
                {
                    var aptNumber = db.Apartment.Find(newTenant.aptID).ApartmentNumber;

                    
                    string string1 = "Hi " + newTenant.FirstName +" "+ newTenant.LastName + ", An Account has been created for you by PointerWebApp.com ";
                    string string2 = "The login information for apartment: "+ aptNumber+" is below";
                    string string3 = "Username: "+newTenant.Username;
                    string string4 = "Temporary password: " + password;
                    string string5 = "Click the on this http://localhost:64083/Account/Manage link and follow the instructions to initiate your account ";
                    string string6 = "Company Description";
                    string string7 = "Find more information...";

                    string x = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", string1, string2, string3, string4, string5, string6, string7);

                    Gmail gmail = new Gmail("pointerwebapp", "Dmc10040!");
                    MailMessage msg = new MailMessage("pointerwebapp@gmail.com", tenantdb.Username);
                    msg.Subject = "Pointer Security New Account Notification";
                    msg.Body = x;
                    gmail.Send(msg);
                }

                var mydata = Json("");

                return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
             
                //return RedirectToAction("ApartmentProfile", new { ApartmentID = newTenant.aptID, BuildingID= newTenant.BuildingID });

            }
            }
                catch(Exception e){

                    ViewBag.Message = e.Message;
                
            }
            var mydata1 = Json("");

            return new JsonResult { Data = mydata1, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }


        public ActionResult resetpwd(string ID)
        {
            var result = PasswordGenerator.ResetPassword(ID);
            var mydata1 = Json(result);

            return new JsonResult { Data = mydata1, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }


        //Tenant Edit
        //[HttpGet]
        //public ActionResult TenantEdit(string TenantID)
        //{
        //    Tenant tn = db.Tenant.Find(TenantID);
        //    return View(tn);        
        //}
       


        

        [HttpGet]
        [AllowAnonymous]
        public JsonResult loadTenant(int apartmentID)
        {   //this i use forthe link since model didn't pass Nav properties

            var tenants = db.Tenant.Where(c => c.aptID == apartmentID).Select(c => new 
            { 
                ID=c.ID,
                FirstName = c.FirstName, 
                LastName = c.LastName, 
                Phone = c.Phone, 
                UserName = c.Username, 
                CreatedDate = c.Created 
            }).ToList();

            var mydata = Json(tenants);

            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
           // return RedirectToAction("ApartmentProfile", new { ApartmentID = ApartmentID, BuildingID =BuildingID});
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult TenantEdit(Tenant model)
        {   //this i use forthe link since model didn't pass Nav properties
           
 
            db.Tenant.Attach(model);
            var Entry = db.Entry(model);
            Entry.Property(c => c.FirstName).IsModified = true;
            Entry.Property(c => c.LastName).IsModified = true;
            Entry.Property(c => c.Phone).IsModified = true;
            Entry.Property(c => c.Username).IsModified = true;
            
            db.SaveChanges();



            var tenants = db.Tenant.Where(c => c.aptID == model.aptID).Select(c => new 
            { 
                ID=c.ID,
                FirstName = c.FirstName, 
                LastName = c.LastName, 
                Phone = c.Phone, 
                UserName = c.Username, 
                CreatedDate = c.Created 
            }).ToList();

            var mydata = Json(tenants);

            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
           // return RedirectToAction("ApartmentProfile", new { ApartmentID = ApartmentID, BuildingID =BuildingID});
        }
        //===================DeleteTenant=============
        //[HttpGet]
        //public ActionResult TenantDelete(string TenantID)
        //{
        //    Tenant tn = db.Tenant.Find(TenantID);
        //    return View(tn);
        //}
        //======================Delete Tenant POST=======================
        [HttpPost]        
        public JsonResult TenantDelete(string TenantID)
        {

            var BID = db.Tenant.Where(c => c.ID == TenantID).FirstOrDefault().Apartment.Buildings.ID;
            var AID = db.Tenant.Where(c => c.ID == TenantID).FirstOrDefault().Apartment.ID;


            try {
              
                
                ApplicationDbContext context = new ApplicationDbContext();
                ApplicationUser AppUser = new ApplicationUser();
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                // remove tenant from role
                userManager.RemoveFromRole(TenantID, "Tenant");
                //find tenant on the tenant table 
                 AspNetUsers aspnetuser = db.AspNetUsers.Find(TenantID);
                db.AspNetUsers.Remove(aspnetuser);
                var tn = db.Tenant.Find(TenantID);
                //remove tenant from the tenant table
                db.Tenant.Remove(tn);
                //save changes 
                db.SaveChanges();
                //load tenant applicationuser 
                AppUser = userManager.FindById(TenantID);
                //delete tenant applicationuser
                var result = userManager.Delete(AppUser);

              
               
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;

            }

            var tenants = db.Tenant.Where(c => c.aptID == (int?)AID).Select(c => new
            {
                ID = c.ID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Phone = c.Phone,
                UserName = c.Username,
                CreatedDate = c.Created
            }).ToList();

            var mydata = Json(tenants);

            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               
        }

        //======================TenantRequest======================
        [HttpGet]
        public ActionResult TenantRequest(string tenantID, int BuildingID ) 
        {

            if (tenantID != null)
            {
                var tenantRequest = new Requests();
                tenantRequest.TenantID = tenantID;
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
        public ActionResult TenantRequest(Requests model, int BuildingID, string TenantID)
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
        public JsonResult getRequestsType(string TenantID)
        {

            var Tomorrow = DateTime.Today.AddDays(1);
            var Today = DateTime.Today.Date;
            //Request From Today
            var todayRequests = db.Requests
                 .Where(c => c.TenantID ==TenantID )
                 .GroupBy(c=> c.RequestType)
                 .Select(c => new ReqTypeCount { key = c.Key, AccessControl = c.Count() }).ToList();



            var dayoftheweek = DateTime.Today.AddDays(3).DayOfWeek;

            return new JsonResult { Data = todayRequests, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


       //History -- requests
        public ActionResult RequestHistoryIndex(string TenantID)
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

        [HttpGet]
    [AllowAnonymous]
        public JsonResult getKeyupSearch(string mysearch, int BuildingID, string aptNumber)
      {

            var Search = new List<TenantSearch>();

            if (mysearch != null || mysearch != "")
            {
                string FN = string.Empty;
                string LN = string.Empty;
                string[] m = mysearch.Split(null);//if the splitting char is null it is assume that the delimiter is a whie space
                var mymanagement = new managementVM();
                if (m.Length > 1)
                {
                    FN = m[0];
                    LN = m[1];

                    Search = db.Tenant
                   .Join(db.Apartment,
                           c => c.aptID,
                           a => a.ID,
                           (c, a) => new { c, a })
                           .Join(db.Buildings,
                           ca => ca.a.BuildingID,
                           b => b.ID,
                           (ca, b) => new TenantSearch
                           {
                               FirstName = ca.c.FirstName,
                               LastName = ca.c.LastName,
                               Apt = ca.a.ApartmentNumber,
                               Address = b.Address,
                               city = b.City,
                               State = b.State,
                               zipcode = b.Zipcode,
                               ApartmentID = ca.a.ID,
                               TenantID = ca.c.ID,
                               Userkey = ca.c.AspNetUsers.Id,
                               Phone = ca.c.Phone,
                               BuildingID = b.ID
                           })
                   .Where(c => c.FirstName.Contains(mysearch) ||
                               c.LastName.Contains(mysearch) ||
                               c.FirstName.Contains(FN) &&
                               c.LastName.Contains(LN)
                          ).Where(c => c.BuildingID == BuildingID)
                    .OrderByDescending(c => c.FirstName)
                   .Take(10)
                   .ToList();
                       if (aptNumber != "")
                    {
                        Search = Search.Where(c => c.Apt == aptNumber).ToList();
                   
                    }

                    return new JsonResult { Data = Search, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                else
                {

                    Search = db.Tenant
                       .Join(db.Apartment,
                         c => c.aptID,
                         a => a.ID,
                         (c, a) => new { c, a })
                         .Join(db.Buildings,
                         ca => ca.a.BuildingID,
                         b => b.ID,
                         (ca, b) => new TenantSearch
                         {
                             FirstName = ca.c.FirstName,
                             LastName = ca.c.LastName,
                             Apt = ca.a.ApartmentNumber,
                             Address = b.Address,
                             city = b.City,
                             State = b.State,
                             zipcode = b.Zipcode,
                             ApartmentID = ca.a.ID,
                             TenantID = ca.c.ID,
                             Userkey = ca.c.AspNetUsers.Id,
                              Phone = ca.c.Phone,
                             BuildingID = b.ID
                         })
                   .Where(c => c.FirstName.Contains(mysearch) || c.LastName.Contains(mysearch))
                   .Where(c => c.BuildingID == BuildingID)
                   .OrderByDescending(c => c.FirstName)
                   .Take(10)
                   .ToList();
                    if (aptNumber != "")
                    {
                        Search = Search.Where(c => c.Apt == aptNumber).ToList();

                    }

                    return new JsonResult { Data = Search, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

            }
            return new JsonResult { Data = Search, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpGet]
        public ActionResult ClientHome()
        {
            ClientHomeVM CH = new ClientHomeVM();
            CH.clients = db.Clients.ToList();
            return View(CH);
        }

        [HttpGet]
        public ActionResult RepairManagement(int buildingID)
        {
            RepairManagement r = new RepairManagement();
           r.RepairsRequests = r.LoadAllRequest(buildingID);
           r.buildingID = buildingID;
           r.building = db.Buildings.Find(buildingID);

            return View(r);
        }
        public JsonResult ReopenRepairTicket(int RepairID)
        {
            RepairManagement r = new RepairManagement();
            r.ReopenRepairTicket(RepairID);
            var mydata = Json("");
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult BuildingRepairResquest(int buildingID)
        {
             RepairManagement r = new RepairManagement();
            var BRR = r.LoadAllRequest(buildingID);
            var mydata = Json(BRR);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult BuildingRepairResquestSortByDateASC(int buildingID)
        {
            RepairManagement r = new RepairManagement();
            var BRR = r.LoadAllRequestSortByDateASC(buildingID);
            var mydata = Json(BRR);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult BuildingRepairResquestSortStatusASC(int buildingID)
        {
            RepairManagement r = new RepairManagement();
            var BRR = r.LoadAllRequestSortStatusASC(buildingID);
            var mydata = Json(BRR);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult BuildingRepairResquestSortUrgencyASC(int buildingID)
        {
            RepairManagement r = new RepairManagement();
            var BRR = r.LoadAllRequestSortUrgencyASC(buildingID);
            var mydata = Json(BRR);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult BuildingRepairResquestSortRequestNumberASC(int buildingID)
        {
            RepairManagement r = new RepairManagement();
            var BRR = r.LoadAllRequestSortRequestNumberASC(buildingID);
            var mydata = Json(BRR);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

       


        [HttpGet]
        public JsonResult SearchbyRequestNumber(string filter, int BuildingID)
        {
            RepairManagement r = new RepairManagement();
            var BRR = r.SearchByRequestNumber(filter, BuildingID);
            var mydata = Json(BRR);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        //Tenant Delivary
        public ActionResult TenantDeliveryIndex(int TenantID)
        {
            return View();        
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult BuildingUsersList(int buildingID)
        {
            var BUL = db.BuildingUser.Select(c => new 
            {
                FullName = c.FirstName + " " + c.LastName,
                Phone = c.Phone,
                Email = c.Email,
                UserID = c.UserID,
                BuildingID = c.BuildingID
            }).ToList();

            var mydata = Json(BUL);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult ClientBuildingList(int ClientID)
        {
            var CBL = db.Buildings.Where(c => c.Clients.ID == ClientID).Select(c => new 
            { 
                BuildingName =c.BuildingName,
                BuildingAddress = c.Address + " " + c.City + ", " + c.State + " " + c.Zipcode,
                Phone = c.BuildingPhone,
                BuildingID = c.ID
    
             }).ToList();

            var mydata = Json(CBL);

            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Search4Building(int ClientID, string Search)
        {
            var CBL = db.Buildings.Where(c => c.Clients.ID == ClientID && c.Address.Contains(Search) || c.Clients.ID == ClientID && c.Zipcode.Contains(Search)).Select(c => new
            {
                BuildingName = c.BuildingName,
                BuildingAddress = c.Address + " " + c.City + ", " + c.State + " " + c.Zipcode,
                Phone = c.BuildingPhone,
                BuildingID = c.ID

            }).ToList();

            var mydata = Json(CBL);

            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult ClientList()
        {
            ClientHomeVM CH = new ClientHomeVM();
            var clientList = db.Clients.Select(c=> new 
            {
                ClientName = c.ClientName, 
                ClientPhone = c.Phone, 
                ClientFullAddress =c.Address +" "+c.City +" "+c.State+" "+c.ZipCode,
                ClientEmail = c.Email,
                ClientFax = c.Fax ,
                ClientAddress = c.Address,
                ClientCity = c.City,
                ClientState = c.State,
                ClientZipcode = c.ZipCode,
                BUildingID = c.ID

            }).ToList();
            var mydata = Json(clientList);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult EditClientAjax(Clients model)
        {

            if (model != null)
            {               
                db.Clients.Attach(model);
                var Entry = db.Entry(model);
                Entry.Property(c => c.ClientName).IsModified = true;
                Entry.Property(c => c.Address).IsModified = true;
                Entry.Property(c => c.City).IsModified = true;
                Entry.Property(c => c.State).IsModified = true;
                Entry.Property(c => c.Phone).IsModified = true;
                Entry.Property(c => c.Fax).IsModified = true;
                Entry.Property(c => c.Email).IsModified = true;
                Entry.Property(c => c.BuildingCount).IsModified = true;
                Entry.Property(c => c.ZipCode).IsModified = true;

                db.SaveChanges();

            }


            var clientList = db.Clients.Select(c => new
            {
                ClientName = c.ClientName,
                ClientPhone = c.Phone,
                ClientFullAddress = c.Address + " " + c.City + " " + c.State + " " + c.ZipCode,
                ClientEmail = c.Email,
                ClientFax = c.Fax,
                ClientAddress = c.Address,
                ClientCity = c.City,
                ClientState = c.State,
                ClientZipcode = c.ZipCode,
                BUildingID = c.ID
            }).ToList();
            var mydata = Json(clientList);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult States()
        {
            var states = db.States.Select(c => new
            {
                Text = c.State, Value = c.ID

            }).ToList();

            var mydata = Json(states);
              return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult SearchClientList(string Search)
        {
            
            var clientList = db.Clients.Where(c=>c.ClientName.Contains(Search)).Select(c => new
            {
                ClientName = c.ClientName,
                ClientPhone = c.Phone,
                ClientFullAddress = c.Address + " " + c.City + " " + c.State + " " + c.ZipCode,
                ClientEmail = c.Email,
                ClientFax = c.Fax,
                ClientAddress = c.Address,
                ClientCity = c.City,
                ClientState = c.State,
                ClientZipcode = c.ZipCode,
                BUildingID = c.ID

            }).ToList();
            var mydata = Json(clientList);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    [HttpGet]
    [AllowAnonymous]
        public ActionResult ClientProfile(int ClientID)
        {

            var c = db.Clients.Find(ClientID);
            ViewBag.ClientID = ClientID;
            ViewBag.ClientName = c.ClientName;
            ViewBag.FullAddress = c.Address + " " + c.City + " " + c.State + " " + c.ZipCode;
            ViewBag.City = c.City;
            ViewBag.State = c.State;
            ViewBag.Zipcode = c.ZipCode;
            ViewBag.Phone = c.Phone;
            ViewBag.Fax = c.Fax;
            ViewBag.Email = c.Email;
            ViewBag.Address = c.Address;

            return View();
        }
        

        /// <summary>
        /// Get Client Contact
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetClientContact(int ClientID)
        {

            var clientContactList = db.ClientContact.Where(c => c.ClientID == ClientID).ToList();
            var mydata = Json(clientContactList);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteClientContact(int ClientContactID, int ClientID)
        {

            ClientContact cc = db.ClientContact.Find(ClientContactID);
            db.ClientContact.Remove(cc);
            db.SaveChanges();

            var clientContactList = db.ClientContact.Where(c => c.ClientID == ClientID).ToList();
            var mydata = Json(clientContactList);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
               
        /// <summary>
        /// Create new client Contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult NewClientContact(ClientContact model)
        {
            if(isajax)
            {


            }
            if (ModelState.IsValid)
            {           
                db.ClientContact.Add(model);
                db.SaveChanges();
            }
            var clientContactList = db.ClientContact.Where(c=>c.ClientID==model.ClientID).ToList();
            var mydata = Json(clientContactList);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult LoadApartments(int buildingID)
        {

            var apartmentdata = db.Apartment
               .Where(c => c.Buildings.ID ==buildingID).Select(c => new { 
                    AptID = c.ID, 
                    Apt = c.ApartmentNumber
                    
                }).ToList();

            var mydata = Json(apartmentdata);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        [AllowAnonymous]
        public JsonResult LoadApartmentsSearch(string search, int BuildingID)
        {

            var apartmentdata = db.Apartment
               .Where(c => c.ApartmentNumber.Contains(search) && c.Buildings.ID==BuildingID).Select(c => new
               {
                   AptID = c.ID,
                   Apt = c.ApartmentNumber

               }).ToList();

            var mydata = Json(apartmentdata);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //To save file, use SaveAs method
                file.SaveAs(Server.MapPath("~/Uploads/") + fileName); //File will be saved in application root
            }
            return Json("Uploaded " + Request.Files.Count + " files");
        }
        //Load building staffs
        public async Task<JsonResult> LoadStaffPermissions(int BuildingID)
        {
            var data = await db.BuildingUser.Where(c => c.BuildingID == BuildingID).Select(c => new {             
            FullName = c.FirstName + " "+ c.LastName,
            Email = c.UserName,
            Phone = c.Phone,
            UserID = c.UserID            
            }).ToListAsync();          
            List<StaffmixPermissions> ObjTPs = new List<StaffmixPermissions>();
            RegisterUsers getuserspermission = new RegisterUsers();  
            foreach(var item in data)
            {
                StaffmixPermissions ObjTP = new StaffmixPermissions();
                ObjTP.FullName = item.FullName;
                ObjTP.Email = item.Email;
                ObjTP.Phone = item.Phone;
                ObjTP.UserID = item.UserID;
                ObjTP.PermisionNames = getuserspermission.LoadBuildingUserPermission(item.UserID);
                ObjTPs.Add(ObjTP);               
            };
            var mydata = Json(ObjTPs);

            return  new  JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
        }

        public JsonResult RepairRequestCat()
        {
            var RRC = db.RepairRequestCategories.Select(c => new { Cat = c.Categories }).ToList();

            var mydata = Json(RRC);
            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }



        [HttpGet]
        public JsonResult TenantEdit(string TenantID)
        {
            Tenant tn = db.Tenant.Find(TenantID);
            Tenant obj = new Tenant();
            obj.ID = tn.ID;
            obj.FirstName = tn.FirstName;
            obj.LastName = tn.LastName;
            obj.Username = tn.Username;
            obj.Phone = tn.Phone;
            var mydata = Json(obj);

            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Searchforusername(string search)
        {

            var Email = db.AspNetUsers.Where(c => c.Email.Contains(search)).Select(c=> new {Email = c.Email}).ToList();
            var mydata = Json(Email);

            return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        
        
      
        //Tenant Messege Center

        public ActionResult RepairModuleManagement(int BuildingID)
        {
            var B = db.Buildings.Find(BuildingID);
            return View(B);
        }
       

        public async Task<JsonResult> loadRequestbysearch(int BuildingID, string filter)
        {
            var repairRequest = await db.RepairRequest
                     .Where(c => c.Tenant.Apartment.Buildings.ID == BuildingID && c.ProblemDescription.Contains(filter))
                     .Select(c => new
                     {
                         RequestedDate = c.RequestedDate,
                         ProblemDescription = c.ProblemDescription,
                         Status = c.Status,
                         ID = c.Id,
                         RequestNumber = c.RepairRequestCategoriesID,
                         Category = c.RepairRequestCategories.Categories,
                         Instruction = c.Instructions_,
                         Urgency = c.RepairUrgency.Urgency,
                         Permision = c.Permissiontoenter,
                         imgUrl = c.PhotoUrl,
                         PrimaryName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                         PrimaryPhone = c.Tenant.Phone,
                         PrimaryEmail = c.Tenant.Username,
                         SecondaryName = c.OtherContactName,
                         SecondaryPhone = c.OtherContactPhone,
                         SecondaryEmail = c.OtherContactEmail
                     }).OrderByDescending(c => c.RequestedDate).ToListAsync();

            var Jsonpackages = Json(repairRequest);



            return new JsonResult { Data = Jsonpackages, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
       [HttpGet]
        public async Task<JsonResult> LoadOpenRepair(int BuildingID)
        {
            var objrepair = await db.RepairRequest.Where(c => c.BuildingID == BuildingID).CountAsync(); ;

            var Jsonpackages = Json(objrepair);
            return new JsonResult { Data = Jsonpackages, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        
  



        [HttpPost]
       public JsonResult ApplyUpdatesRepairRequest(string UserID, int RepairRequestID, string Notes, bool whichone)
       {

           if (whichone == true) //Building Staff========================================================================
           {
               pdfWorker pdfworker = new pdfWorker();
               RepairRequest RR = db.RepairRequest.Find(RepairRequestID);

               RepairRequestNote RN = new RepairRequestNote
               {
                   DateCreated = DateTime.Now,
                   Notes = Notes
               };

               db.RepairRequestNote.Add(RN);
               db.SaveChanges();

               RR.RepairRequestNoteID = RN.Id;
               RR.AssignID = UserID;
               RR.AssignContractorID = null;
               RR.Status = "Assigned";

               db.RepairRequest.Attach(RR);
               var Entry = db.Entry(RR);

               Entry.Property(c => c.RepairRequestNoteID).IsModified = true;
               Entry.Property(c => c.AssignID).IsModified = true;
               Entry.Property(c => c.AssignContractorID).IsModified = true;
               Entry.Property(c => c.Status).IsModified = true;

               db.SaveChanges();

               var Worker = db.BuildingUser.Where(c => c.UserID == UserID).FirstOrDefault();

               //string string1 = "<div style='font-size:20px; colo:blue; display:bloc'>Hi " + Worker.FirstName + " " + Worker.LastName + ",</div> ";
               //string string2 = "You have a new assignemt and the description is bellow:";
               //string string3 = "The Category of this Request is " + RR.RepairRequestCategories.Categories;
               //string string4 = "The Decription of the request is: " + RR.ProblemDescription;
               //string string5 = "The Urgency is: " + RR.RepairUrgency.Urgency;
               //string string6 = "For questions about this email Contact management at: " + RR.Buildings.BuildingPhone;
               //string string7 = "Find more information...";

               //string x = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", string1, string2, string3, string4, string5, string6, string7);

               string contenttobemail = " <div style='font-size:20px; display:block;  width:100%; background:#0071bc;  height:50px;  line-height:50px; padding:0 15px; border:1px solid lightgrey;   color:white;' >"+
                Worker.FirstName + " " + Worker.LastName +"</div>"+
                                   "<div style='   display:block;   width:100%;   margin:10px 0 10px 0;   padding:10px 15px;   background:#F0F0F0;   border:1px solid lightgrey;   '>     You have a new assignemt and the description is bellow:<br/>"+
                                   " <hr/>"+
                                   " <br/>"+
                                   " <b> The Category of this Request is</b> "+
                                   "<br/>"+
                                   RR.RepairRequestCategories.Categories +
                                   " <hr/>"+
                                   " <br/>"+
                                   "<b>The Urgency is:</b>"+
                                   " <br/>" + RR.RepairUrgency.Urgency +
                                   "<hr/>"+
                                   " <br/>"+
                                   " <b>The Decription of the request is:</b>"+
                                   "<br/>"+
                                  RR.ProblemDescription+
                                   " <hr/>"+
                                   "<br/>"+
                                   "</div>"+
                                   "<div style='font-size:20px; display:block; width:100%; background:#0071bc; height:50px;line-height:50px; padding:0 15px; border:1px solid lightgrey; color:white;' >For questions about this email Contact management at: " + RR.Buildings.BuildingPhone + "Find more information...  </div>";

               Gmail gmail = new Gmail("pointerwebapp", "dmc10040");
               MailMessage msg = new MailMessage("pointerwebapp@gmail.com", Worker.Email);
               msg.Subject = "New Assignment Notification";
               msg.Body = contenttobemail;
               msg.IsBodyHtml = true;
               
               //new
               PdfContractContent pdfContent = new PdfContractContent {
                   Address = RR.Buildings.Address + " " + RR.Tenant.Apartment.ApartmentNumber + " " + RR.Buildings.City +" " + RR.Buildings.State+" " + RR.Buildings.Zipcode,
                 Category = RR.RepairRequestCategories.Categories,
                 Priority = RR.RepairUrgency.Urgency,
                 Status = RR.Status,
                 Issued = RR.RequestedDate.Month.ToString() +"/"+ RR.RequestedDate.Day.ToString() +"/"+ RR.RequestedDate.Year.ToString(),
                 primaryContact = RR.Tenant.FirstName + " " + RR.Tenant.LastName,
                 PrimaryPhone = RR.Tenant.Phone,
                 PrimaryEmail = RR.Tenant.Username,
                 OfficeNotes = RR.RepairRequestNote.Notes,
                 RequestNumber =RR.RequestNumber,
                 problem = RR.ProblemDescription,
                 TenantInstruction = RR.Instructions_   
               };
               //new
               var result = pdfworker.CreateTable1(Server.MapPath("~/ContractPDF/"), Server.MapPath("~/img/"), Server.MapPath("~/fonts/"), pdfContent);
              
               Attachment attachment;
               attachment = new Attachment(Server.MapPath("~/ContractPDF/newContractFile.pdf"));
               msg.Attachments.Add(attachment);     

               gmail.Send(msg);
               attachment.Dispose();//needs to be dispose because the process is use and cannot be open twice at the same time.
               msg.Dispose();
           }
           else if (whichone == false) //Company========================================================================================================
           {
               pdfWorker pdfworker = new pdfWorker();
               RepairRequest RR = db.RepairRequest.Find(RepairRequestID);

               RepairRequestNote RN = new RepairRequestNote
               {
                   DateCreated = DateTime.Now,
                   Notes = Notes

               };

               db.RepairRequestNote.Add(RN);
               db.SaveChanges();

               RR.RepairRequestNoteID = RN.Id;
               RR.AssignContractorID = UserID;
               RR.AssignID = null;
               RR.Status = "Assigned";

               db.RepairRequest.Attach(RR);
               var Entry = db.Entry(RR);

               Entry.Property(c => c.RepairRequestNoteID).IsModified = true;
               Entry.Property(c => c.AssignContractorID).IsModified = true;
               Entry.Property(c => c.AssignID).IsModified = true;
               Entry.Property(c => c.Status).IsModified = true;
               db.SaveChanges();

               var Worker = db.Contractor.Where(c => c.Id == UserID).FirstOrDefault();

               string contenttobemail = " <div style='font-size:20px; display:block;  width:100%; background:#0071bc;  height:50px;  line-height:50px; padding:0 15px; border:1px solid lightgrey;   color:white;' >" +
                 Worker.CompanyName + "</div>" +
                                        "<div style='   display:block;   width:100%;   margin:10px 0 10px 0;   padding:10px 15px;   background:#F0F0F0;   border:1px solid lightgrey;   '>     You have a new assignemt and the description is bellow:<br/>" +
                                        " <hr/>" +
                                        " <br/>" +
                                        " <b> The Category of this Request is</b> " +
                                        "<br/>" +
                                        RR.RepairRequestCategories.Categories +
                                        " <hr/>" +
                                        " <br/>" +
                                        "<b>The Urgency is:</b>" +
                                        " <br/>" + RR.RepairUrgency.Urgency +
                                        "<hr/>" +
                                        " <br/>" +
                                        " <b>The Decription of the request is:</b>" +
                                        "<br/>" +
                                       RR.ProblemDescription +
                                        " <hr/>" +
                                        "<br/>" +
                                        "</div>" +
                                        "<div style='font-size:20px; display:block; width:100%; background:#0071bc; height:50px;line-height:50px; padding:0 15px; border:1px solid lightgrey; color:white;' >For questions about this email Contact management at: " + RR.Buildings.BuildingPhone + "Find more information...  </div>";


               Gmail gmail = new Gmail("pointerwebapp", "dmc10040");
               MailMessage msg = new MailMessage("pointerwebapp@gmail.com", Worker.Email);
               msg.Subject = "New Assignment Notification";
               msg.Body = contenttobemail;

               msg.IsBodyHtml = true;
               //new
               PdfContractContent pdfContent = new PdfContractContent
               {
                   Address = RR.Buildings.Address + " " + RR.Tenant.Apartment.ApartmentNumber + " " + RR.Buildings.City + " " + RR.Buildings.State + " " + RR.Buildings.Zipcode,
                   Category = RR.RepairRequestCategories.Categories,
                   Priority = RR.RepairUrgency.Urgency,
                   Status = RR.Status,
                   Issued = RR.RequestedDate.Month.ToString() + "/" + RR.RequestedDate.Day.ToString() + "/" + RR.RequestedDate.Year.ToString(),
                   primaryContact = RR.Tenant.FirstName + " " + RR.Tenant.LastName,
                   PrimaryPhone = RR.Tenant.Phone,
                   PrimaryEmail = RR.Tenant.Username,
                   OfficeNotes = RR.RepairRequestNote.Notes,
                   RequestNumber =RR.RequestNumber,
                   problem = RR.ProblemDescription,
                   TenantInstruction = RR.Instructions_
               };
               //new
               var result = pdfworker.CreateTable1(Server.MapPath("~/ContractPDF/"), Server.MapPath("~/img/"), Server.MapPath("~/fonts/"), pdfContent);

               Attachment attachment;
               attachment = new Attachment(Server.MapPath("~/ContractPDF/newContractFile.pdf"));
               msg.Attachments.Add(attachment); 
               gmail.Send(msg);
               attachment.Dispose();
           
           }
            
           var JSONdATA = Json("");
           return new JsonResult {Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
       }
        [HttpPost]
        public JsonResult CloseRepairTicket(int ID, string ClosingComments)
        {
           
            RepairRequest RR = db.RepairRequest.Find(ID);
           
           
            db.RepairRequest.Attach(RR);
            var Entry = db.Entry(RR);
            RR.Resolution = ClosingComments;
            RR.Status = "Close";
            Entry.Property(c => c.Resolution).IsModified = true;
            Entry.Property(c => c.Status).IsModified = true;
            db.SaveChanges();

            var JSONdATA = Json("");
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        

            [AllowAnonymous]
        public JsonResult LoadCloseRequestsbaseonsearch(int buildingID, string filterRequestNumber)
        {
           

            RepairManagement OBJRM = new RepairManagement();

            var ListOfCloseRequests = OBJRM.LoadCloseRequestsbaseonsearch(buildingID, filterRequestNumber);

            var JSONdATA = Json(ListOfCloseRequests);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [AllowAnonymous]
        public JsonResult LoadCloseRequests(int buildingID)
        {
           

            RepairManagement OBJRM = new RepairManagement();

            var ListOfCloseRequests = OBJRM.LoadAllCloseRequest(buildingID);

            var JSONdATA = Json(ListOfCloseRequests);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        [HttpGet]
        public JsonResult LoadingAssignTo(string UserID)
        {

            var u = db.BuildingUser
                .Where(c => c.UserID == UserID)
                .Select(c => new 
                {   ID = c.Id, 
                    AspNetUserID = c.UserID, 
                    FirstName = c.FirstName,
                    LastName = c.LastName, 
                    Phone = c.Phone, 
                    UserName = c.UserName 
                }).FirstOrDefault();

            var JSONdATA = Json(u);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
           
        }
        /// <summary>
        /// Create a New Contractor and return a list of contractor
        /// </summary>
        /// <param name="model">Accept a NewContractor Model</param>
        /// <returns>Return a Json Object</returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult InsertNewContractor(NewContractor model)
        {
            //1)creat aspnetuserID
            string password = PasswordGenerator.GeneratePassword("8").ToString();
            //2) insert user
            var UserID = RegisterUsers.InsertUser(model.Email, model.CMainPhone, password);
            //3) insert User Role Contractor
            var result = RegisterUsers.InserToRole("Contractor", UserID);
            //4) add Contractor
            //Transfer
            Contractor objC = new Contractor
            {
                 Id = UserID,
                 CompanyName = model.CName,
                 Address = model.CAddress,
                 City = model.CCity,
                 State = model.CState,
                 Zipcode = model.CZipcode,
                 Phone = model.CMainPhone,
                 ContactName = model.PName,
                 ContractPhone = model.PPhone,
                 Comments = model.Comments,
                 Email = model.Email,
                 SendNewPass = model.SendNewPassword,
                 CategoryName = model.CatName,
                 BuildingID = model.BuildingID                   
            };

            db.Contractor.Add(objC);
            db.SaveChanges();

            var objList = db.Contractor.Where(c=>c.BuildingID == objC.BuildingID).Select(c => new {
                      ID = c.Id,
                      CompanyName = c.CompanyName,
                      Address = c.Address,
                      City = c.City,
                      State = c.State,
                      Zipcode = c.Zipcode,
                      Phone = c.Phone,
                      ContactName = c.ContactName,
                      ContactPhone = c.ContractPhone,
                      Comments = c.Comments,
                      Email = c.Email,
                      SendNewPassword = c.SendNewPass,
                      Category = c.CategoryName,
                      BuildingID = c.BuildingID
            }).ToList();
            var JSONdATA = Json(objList);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };          
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoadContractors(int BuildingID)
        {
              var objList = db.Contractor.Where(c=>c.BuildingID == BuildingID).Select(c => new {
                      ID = c.Id,
                      CompanyName = c.CompanyName,
                      Address = c.Address,
                      City = c.City,
                      State = c.State,
                      Zipcode = c.Zipcode,
                      Phone = c.Phone,
                      ContactName = c.ContactName,
                      ContactPhone = c.ContractPhone,
                      Comments = c.Comments,
                      Email = c.Email,
                      SendNewPassword = c.SendNewPass,
                      Category = c.CategoryName,
                      BuildingID = c.BuildingID
            }).ToList();
            var JSONdATA = Json(objList);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };          
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult UpdateContractor(NewContractor model)
        {
            Contractor objC = new Contractor
            {
                Id = model.ASPNETUSERID,
                CompanyName = model.CName,
                Address = model.CAddress,
                City = model.CCity,
                State = model.CState,
                Zipcode = model.CZipcode,
                Phone = model.CMainPhone,
                ContactName = model.PName,
                ContractPhone = model.PPhone,
                Comments = model.Comments,
                Email = model.Email,
                SendNewPass = model.SendNewPassword,
                CategoryName = model.CatName,
                BuildingID = model.BuildingID
            };

            db.Contractor.Attach(objC);
            var Entry = db.Entry(objC);
            Entry.Property(c => c.CategoryName).IsModified = true;
            Entry.Property(c => c.Address).IsModified = true;
            Entry.Property(c => c.State).IsModified = true;
            Entry.Property(c => c.Zipcode).IsModified = true;
            Entry.Property(c => c.Phone).IsModified = true;
            Entry.Property(c => c.ContactName).IsModified = true;
            Entry.Property(c => c.ContractPhone).IsModified = true;
            Entry.Property(c => c.Email).IsModified = true;
            Entry.Property(c => c.CategoryName).IsModified = true;

            db.SaveChanges();
          

            var objList = db.Contractor.Where(c => c.BuildingID == model.BuildingID).Select(c => new
            {
                ID = c.Id,
                CompanyName = c.CompanyName,
                Address = c.Address,
                City = c.City,
                State = c.State,
                Zipcode = c.Zipcode,
                Phone = c.Phone,
                ContactName = c.ContactName,
                ContactPhone = c.ContractPhone,
                Comments = c.Comments,
                Email = c.Email,
                SendNewPassword = c.SendNewPass,
                Category = c.CategoryName,
                BuildingID = c.BuildingID
            }).ToList();
            var JSONdATA = Json(objList);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };          
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult InsertRepairTechNote(RepairTechNote model)
        {
           if(ModelState.IsValid)
           {
               model.CreatedDate = DateTime.Today;
               db.RepairTechNote.Add(model);
               db.SaveChanges();
           }

        var returndata =   db.RepairTechNote.Where(c => c.RepairRequestID == model.RepairRequestID).Select(c => new {id = c.Id, RepairRequestID = c.RepairRequestID, Notes = c.Notes, CreatedDate = c.CreatedDate }).ToList();
        var JSONdATA = Json(returndata);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult LoadRepairTechNotes(int RequestID)
        {
            var returndata = db.RepairTechNote.Where(c => c.RepairRequestID == RequestID).Select(c => new { id = c.Id, RepairRequestID = c.RepairRequestID, Notes = c.Notes, CreatedDate = c.CreatedDate }).ToList();
            var JSONdATA = Json(returndata);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public ActionResult DeleteRepairTechNote(int RequestID)
        {
            var obj = db.RepairTechNote.Where(c => c.RepairRequestID == RequestID).FirstOrDefault();
            db.RepairTechNote.Remove(obj);
            db.SaveChanges();
            var returndata = db.RepairTechNote.Where(c => c.RepairRequestID == RequestID).Select(c => new { id = c.Id, RepairRequestID = c.RepairRequestID, Notes = c.Notes, CreatedDate = c.CreatedDate }).ToList();
            var JSONdATA = Json(returndata);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }







        public bool isajax { get; set; }
    }
}

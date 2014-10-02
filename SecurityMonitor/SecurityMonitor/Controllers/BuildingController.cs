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


namespace SecurityMonitor.Controllers
{
    public class BuildingController : Controller
    {
        PointersecurityEntities1 db = new PointersecurityEntities1();


        public ActionResult ClientIndex()
        {
            var clients = db.Clients
              
               .Select(c => new ClientsVM
               {
                   ID = c.ID,
                   ClientName = c.ClientName,
                   BuildingCount = (int)c.BuildingCount
               });
            return View(clients);
        }
        
        [HttpGet]
        public ActionResult AddClient()
        {

           
            return View(); 
        }

        [HttpPost]
        public ActionResult AddClient(ClientsVM newClient)
        {
            try 
            {
                if(ModelState.IsValid)
                {
                    var newclient = new Client
                    {
                        ClientName = newClient.ClientName,
                        BuildingCount = newClient.BuildingCount
                    };

                    db.Clients.Add(newclient);
                    db.SaveChanges();
            }
         } 
            catch(ExecutionEngineException e)
            {
                ViewBag.ErrorMessage = e.Message;
                return null;
            }

            return RedirectToAction("ClientIndex");
        }


        public ActionResult BuildingIndex(int ClientID) 
        {
            Session["ClientID"] = ClientID;
            var building = db.Buildings
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
                }).ToList();
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
        public ActionResult AddBuilding(BuildingInfoVM apartmentvm)
        {if (ModelState.IsValid)
            {
                var apartment = new Building
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
                db.SaveChanges();
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
        public ActionResult AddApartment(ApartmentVM apartmentvm)
        {
            if (ModelState.IsValid)            
            { var apartment = new Apartment
                {ApartmentNumber = apartmentvm.ApartmentNumber,
                  BuildingID = Convert.ToInt32(apartmentvm.BuildingID),
                  FloorNumber = apartmentvm.FloorNumber
                };
               db.Apartments.Add(apartment);
               db.SaveChanges();
            }
           return RedirectToAction("BuildingProfile", "Building");
        }


        //===================== Apartments CSV Import=======================
        public ActionResult ProcessCsv(EventItem[] model)
        {
            int BuildingID = (int)Session["BuildingID"];

            if (ModelState.IsValid)
            {
               foreach(var item in model)
               { 
                   var apartment = new Apartment
                    {
                        ApartmentNumber = item.AparmentNumber,
                        BuildingID = BuildingID,
                        FloorNumber = item.Floor
                    };
                    db.Apartments.Add(apartment);
                    db.SaveChanges(); 
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
        public ActionResult BuildingProfile(int? BuildingID)
        {
            if (BuildingID != null)
            {
                Session["BuildingID"] = BuildingID;
            }
            else
            {
                BuildingID = (int)Session["BuildingID"];
            }
            var buildinginfo = db.Buildings
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
                  }).First();

            ViewBag.buildingInfo = buildinginfo;

            //building profile appartmentlist
            var apartmentlist = db.Apartments
                .Where(c => c.BuildingID == BuildingID)
                .Select(c => new ApartmentVM { 
                 ApartmentNumber = c.ApartmentNumber
           
                }).ToList();
            ViewBag.apartmentlist = apartmentlist;



            

            return View( );
        }

        //=====================Activity Log=======================
        [HttpGet]
        public ActionResult ActivityPartial(int? page, string searchBy, string search)
        {

            var currentUserID = User.Identity.GetUserId();
            if (Request.HttpMethod != "GET")
            {
                page = 1; // after post reset page to 1

            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            //=================user activity===============================
            var myUserActivitiesLogVM = new UserActivityLogVM();
            if (User.Identity.IsAuthenticated != false)
            {
                string myUserID = User.Identity.GetUserId().ToString();
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


                //=============Search=================
                if (searchBy == "Function")
                {
                    myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                               .Where(UAL => UAL.UserID == myUserID && UAL.Function_Performed.Contains(search) || search == null)
                               .Select(UAL => new ActivityLog
                               {
                                   UserID = UAL.UserID,
                                   ID = UAL.ID,
                                   DateCreated = UAL.DateOfEvent,
                                   FunctionPerformed = UAL.Function_Performed,
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
                        myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                                   .Where(UAL => UAL.UserID == myUserID && UAL.DateOfEvent == theTime)
                                   .Select(UAL => new ActivityLog
                                   {
                                       UserID = UAL.UserID,
                                       ID = UAL.ID,
                                       DateCreated = UAL.DateOfEvent,
                                       FunctionPerformed = UAL.Function_Performed,
                                       Message = UAL.Message
                                   }).ToList();
                    }
                    else
                    {
                        ViewBag.ItisNotaDay = search;

                        myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                             .Where(UAL => UAL.UserID == myUserID)
                             .Select(UAL => new ActivityLog
                             {
                                 UserID = UAL.UserID,
                                 ID = UAL.ID,
                                 DateCreated = UAL.DateOfEvent,
                                 FunctionPerformed = UAL.Function_Performed,
                                 Message = UAL.Message,

                             }).ToList();
                    }
                }
                else
                {
                    myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                              .Where(UAL => UAL.UserID == myUserID)
                              .Select(UAL => new ActivityLog
                              {
                                  UserID = UAL.UserID,
                                  ID = UAL.ID,
                                  DateCreated = UAL.DateOfEvent,
                                  FunctionPerformed = UAL.Function_Performed,
                                  Message = UAL.Message,

                              }).ToList();
                }
                ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM;
            }
            return PartialView(myUserActivitiesLogVM.UserActivites.ToPagedList(pageNumber, pageSize));
        }


        //====================apartmenprofile ==========-==========

        [HttpGet]
        public ActionResult ApparmentProfile(int? ApartmentID) 
        {


            return View();
        }
       



    }
}
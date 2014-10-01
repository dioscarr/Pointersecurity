using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models;
using SecurityMonitor.Models.EntityFrameworkFL;
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
                    ClientID = c.ClientID
                }).ToList();
            return View(building);
        }
        // GET: Building
        [HttpGet]
        public ActionResult AddBuilding()
        {
            var id = Session["ClientID"];
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
           return RedirectToAction("Userprofile", "userVMs");
        }


        //===================== Apartments CSV Import=======================
        public ActionResult ProcessCsv(EventItem[] model)
        {


            if (ModelState.IsValid)
            {
               foreach(var item in model)
               { 
                   var apartment = new Apartment
                    {
                        ApartmentNumber = item.AparmentNumber,
                        BuildingID = item.BuildingID,
                        FloorNumber = item.Floor
                    };
                    db.Apartments.Add(apartment);
                    db.SaveChanges(); 
               }
            }
            
            return RedirectToAction("Userprofile", "userVMs");
        }

        public ViewResult Show()
        {
            return View();
        }





    }
}
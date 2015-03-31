using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models;
using Doormandondemand;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;

namespace SecurityMonitor.Controllers
{
   [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {

         //DB context
        PointerdbEntities db = new PointerdbEntities();
        //Building Profile ==================================================================================
        


        [HttpGet]        
        public ActionResult Index()
        {
         return View();
        }
         [HttpGet]
        public async Task<ActionResult> BuildingProfile(int? page, string search, int? BuildingID)
        {
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
            //building profile appartmentlist=
            if (page == null  && search != null){ViewBag.search = search;}
            if (page != null && search != null){ViewBag.search = search;}
            if (Request.HttpMethod != "GET"){page = 1;  }
            int pageSize = 96;
            int pageNumber = (page ?? 1);
            if (search !=null){
                //executes when there is a search
                var apartmentlist = await db.Apartment
               .Where(c => c.BuildingID == BuildingID && c.ApartmentNumber.Contains(search))
               .Select(c => new ApartmentVM
               {ID = c.ID, ApartmentNumber = c.ApartmentNumber }).ToListAsync();
                ViewBag.apartmentlist = apartmentlist.ToPagedList(pageNumber, pageSize);
            }
            else
            {
                //executes when there is no search
                var apartmentlist = await db.Apartment
                    .Where(c => c.BuildingID == BuildingID)
                    .Select(c => new ApartmentVM
                    { ApartmentNumber = c.ApartmentNumber, ID = c.ID }).ToListAsync();
                ViewBag.apartmentlist = apartmentlist.ToPagedList(pageNumber, pageSize);
            }
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
            var apartmentprofile = await db.Apartment
                                            .Where(a => a.ID == ApartmentID)
                                            .Select(c => new ApartmentVM
                                            {
                                                ApartmentNumber = c.ApartmentNumber,
                                                FloorNumber = c.FloorNumber,
                                                BuildingID = (int)c.BuildingID,
                                                ID = c.ID
                                            }).ToListAsync();

            var tenant = await db.Tenant
                .Where(t => t.aptID == ApartmentID).ToListAsync();

            ViewBag.tenant = tenant;

            return View(apartmentprofile);
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doormandondemand;
using SecurityMonitor.Models;
using System.Net.Http;
using System.Net;
using SecurityMonitor;
using System.Threading.Tasks;
using SecurityMonitor.Workes;


namespace SecurityMonitor.Controllers
{
       [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        PointerdbEntities db = new PointerdbEntities();
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
        // -===============Timeline for requests=================
        public JsonResult getTodayRequests(string sortby)
        {
            var Next7Days = DateTime.Today.AddDays(7);
            var Tomorrow = DateTime.Today.AddDays(1);
            var Today = DateTime.Today.Date;

            List<RequestForTodayVM> todayRequests = new List<RequestForTodayVM>();

          
                if (sortby =="Category")
                { 
                //logic for sorting
                //Request From Today
                todayRequests = db.Requests
                    .Where(c => c.FromDate >= Today && c.FromDate < Tomorrow || c.FromDate < Today && c.ToDate >= Today)
                  .Join(db.Tenant, c => c.TenantID, t => t.ID,
                  (c, t) => new RequestForTodayVM { Type = c.RequestType, Description = c.Description, From = c.FromDate, To = c.ToDate })
                    .ToList();
                }
                else if (sortby == "Tenant")
                {
                    //logic for sorting
                    //Request From Today
                    todayRequests = db.Requests
                        .Where(c => c.FromDate >= Today && c.FromDate < Tomorrow || c.FromDate < Today && c.ToDate >= Today)
                      .Join(db.Tenant, c => c.TenantID, t => t.ID,
                      (c, t) => new RequestForTodayVM { Type =c.ID +" "+ t.FirstName + " " + t.LastName, Description = c.Description, From = c.FromDate, To = c.ToDate })
                        .ToList();
                }
                else if (sortby == "next7days")
                {
                    //logic for sorting
                    
                    todayRequests = db.Requests
                        .Where(c => c.FromDate >= Today && c.FromDate <= Next7Days || c.FromDate < Today && c.ToDate >= Next7Days)
                      .Join(db.Tenant, c => c.TenantID, t => t.ID,
                      (c, t) => new RequestForTodayVM { Type = c.ID +" "+ t.FirstName + " " + t.LastName,
                          Description = "--From-- "+ c.FromDate+ " --TO-- "+ c.ToDate + 
                          "--details--: "+c.Description,
                          From = c.FromDate, 
                          To = c.ToDate })
                        .ToList();
                    foreach (var item in todayRequests)
                    {
                        if (item.From >= Tomorrow)
                        {
                            //do nothing
                        }
                        else if (item.To <= Next7Days)
                        { 
                            //do nothing
                        }
                        else
                        {
                            item.From = Tomorrow;
                        }
                    }
                }


                else if (sortby == null)
                {

                    //Request From Today
                    todayRequests = db.Requests
                        .Where(c => c.FromDate >= Today && c.FromDate < Tomorrow || c.FromDate < Today && c.ToDate >= Today)
                      .Join(db.Tenant, c => c.TenantID, t => t.ID,
                      (c, t) => new RequestForTodayVM { Type = c.ID + " " + c.RequestType, Description = c.Description, From = c.FromDate, To = c.ToDate })
                        .ToList();
            
                }
          
          
            
           


            var dayoftheweek = DateTime.Today.AddDays(3).DayOfWeek;

            return new JsonResult { Data = todayRequests, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //==================Manage Request Type GET========================
        [HttpGet]
        public ActionResult manageReqType(managementVM managementModel)
        {

           

            LoadReqType();
            return View(managementModel);
        }
        //==================Manage Request Type POST========================
        public void LoadReqType() {

            List<SelectListItem> reqtypelist = new List<SelectListItem>();
            var myitems = db.ReqType.ToList();

            foreach (var item in myitems)
            {
                reqtypelist.Add(new SelectListItem { Text = item.ReqType1, Value = item.ID.ToString() });

            }
            ViewBag.reqType = reqtypelist;
        
        }
        [HttpPost]
        public ActionResult manageReqType(managementVM management, int? ReqTypeID, string Search)
        {

           
           
            if (ModelState.IsValid)
            {
                if (management.ReqType != null)
                {
                    var reqtype = new ReqType
                    {
                        ReqType1 = management.ReqType.ReqType1
                    };
                    db.ReqType.Add(reqtype);
                    db.SaveChanges();
                    LoadReqType();
                    return RedirectToAction("manageReqType");
                }

                else if (ReqTypeID != null)
                {
                    var mydeletetype = db.ReqType.Find(ReqTypeID);
                    db.ReqType.Remove(mydeletetype);
                    db.SaveChanges();
                    LoadReqType();

                    return RedirectToAction("manageReqType");
                  
                }
                else if (Search != null || Search != "")
                {
                    string FN = string.Empty;
                    string LN = string.Empty;
                   string[] m = Search.Split(null);//if the splitting char is null it is assume that the delimiter is a whie space
                   var mymanagement = new managementVM();
                   if (m.Length > 1)
                   {
                       FN = m[0];
                       LN = m[1];

                       mymanagement.Tenants = db.Tenant.Where(c => c.FirstName.Contains(Search) || c.LastName.Contains(Search) || c.FirstName.Contains(FN) && c.LastName.Contains(LN))
                        .Select(c => new TenantVM { ID = c.ID, FirstName = c.FirstName, LastName = c.LastName, aptID = (int)c.aptID, Phone = c.Phone, created = c.Created }).ToList();
                   }
                   else
                   {
                       mymanagement.Tenants = db.Tenant.Where(c => c.FirstName.Contains(Search) || c.LastName.Contains(Search))
                           .Select(c => new TenantVM { ID = c.ID, FirstName = c.FirstName, LastName = c.LastName, aptID = (int)c.aptID, Phone = c.Phone, created = c.Created }).ToList();
                   }
                    

                   
                  




                    LoadReqType();
                    return View(mymanagement);                             
                  
                }
                 
            }
            

            return View();
        }
        //this is use forthe search guess list
        public JsonResult getKeyupSearch(string mysearch)        {

            var Search = new List<TenantSearch>();

            if(mysearch!=null || mysearch!="")
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
                              (c, a) => new { c,a})
                              .Join(db.Buildings,
                              ca=> ca.a.BuildingID,
                              b=> b.ID,
                              (ca, b) => new TenantSearch
                                                          {  
                                                            FirstName = ca.c.FirstName,
                                                            LastName = ca.c.LastName,
                                                            Apt = ca.a.ApartmentNumber,
                                                            Address = b.Address,
                                                            city = b.City,
                                                            zipcode = b.Zipcode,
                                                            ApartmentID = ca.a.ID,
                                                            TenantID = ca.c.ID,
                                                            
                                                            BuildingID = b.ID
                                                          })
                      .Where(c => c.FirstName.Contains(mysearch) ||
                                  c.LastName.Contains(mysearch) ||
                                  c.FirstName.Contains(FN) && 
                                  c.LastName.Contains(LN)
                             )
                       .OrderByDescending(c=> c.FirstName)
                      .Take(10)
                      .ToList();
                            
                     
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
                                                            zipcode = b.Zipcode,
                                                            ApartmentID = ca.a.ID,
                                                            TenantID = ca.c.ID,
                                                            BuildingID = b.ID
                                                        })
                      .Where(c => c.FirstName.Contains(mysearch) || c.LastName.Contains(mysearch))
                      .OrderByDescending(c=> c.FirstName)
                      .Take(10)
                      .ToList();
                        
                       return new JsonResult { Data = Search, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                   }
             
        }
              return new JsonResult { Data = Search, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        
        public ActionResult TypeMasterProfile()
        {
            List<SelectListItem> n = new List<SelectListItem>();
           

            ViewBag.listItems = n;
            

           
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> FromJson(List<JsonVM> name)
        {


            foreach(var item in name)
            {
                
                switch (item.Controller)
            {
                case "1":


                   var mppt1 = new MasterProfileFields {
                       Controller = item.Controller,
                        Label = item.TextboxLabel
                   };

                   db.MasterProfileFields.Add(mppt1);
                  await db.SaveChangesAsync();

                    break;
                case "2":
                     var mppt2 = new MasterProfileFields {
                         Controller = item.Controller,
                        Label = item.TextboxLabel
                   };

                   db.MasterProfileFields.Add(mppt2);
                   await db.SaveChangesAsync();
               
                    break;
                case "3":
                     var mppt3 = new MasterProfileFields {
                         Controller = item.Controller,
                        Label = item.TextboxLabel
                   };

                   db.MasterProfileFields.Add(mppt3);
                   await db.SaveChangesAsync();
                
                    break;
                case "4":
                     var mppt4 = new MasterProfileFields {
                         Controller = item.Controller,
                        Label = item.TextboxLabel
                   };

                   db.MasterProfileFields.Add(mppt4);
                   await db.SaveChangesAsync();
               
                    break;
                case "5":
                     var mppt5 = new MasterProfileFields {
                         Controller = item.Controller,
                        Label = item.TextboxLabel
                   };

                   db.MasterProfileFields.Add(mppt5);
                  await db.SaveChangesAsync();
                
                    break;

            }

            
            }

          
            
            
            
            // Can process the data any way we want here,
            // e.g., further server-side validation, save to database, etc
            return Json(name);
        }

        [HttpGet]
        public ActionResult PreviewTemplate()
        {
            var MPPT = db.MasterProfileFields
                .Select(mppt => new MasterProfileVM {  Label= mppt.Label, Controller =mppt.Controller, ID = mppt.ID}).ToList();


            return View(MPPT);
        }

         [HttpGet]
        public JsonResult LoadSelectedDate(string SelectedDate)
        {
            Calendarworkers CW = new Calendarworkers();
            var returnDate = CW.loadSelectedRequests(SelectedDate);

            var JSONdATA = Json(returnDate);
            return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

         [HttpGet]
         public JsonResult LoadCurrenttMonth()
         {

             Calendarworkers CW = new Calendarworkers();
             var returnDate = CW.loadForTheMonth();

             var JSONdATA = Json(returnDate);
             return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         }

      

    }
}

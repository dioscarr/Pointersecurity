using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models.EntityFrameworkFL;
using SecurityMonitor.Models;

namespace SecurityMonitor.Controllers
{
    public class DashBoardController : Controller
    {
        PointersecurityEntities1 db = new PointersecurityEntities1();
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
        // -===============Timeline for requests=================
        public JsonResult getTodayRequests()
        {

            var Tomorrow = DateTime.Today.AddDays(1);
            var Today = DateTime.Today.Date;
            //Request From Today
            var todayRequests = db.Requests
                 .Where(c => c.FromDate >= Today && c.FromDate < Tomorrow || c.FromDate < Today && c.ToDate >= Today)
                 .Select(c => new { Type = c.RequestType, Description = c.Description, From = c.FromDate, To = c.ToDate }).ToList();


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
            var myitems = db.ReqTypes.ToList();

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
                    db.ReqTypes.Add(reqtype);
                    db.SaveChanges();
                    LoadReqType();
                    return RedirectToAction("manageReqType");
                }

                else if (ReqTypeID != null)
                {
                    var mydeletetype = db.ReqTypes.Find(ReqTypeID);
                    db.ReqTypes.Remove(mydeletetype);
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

                       mymanagement.Tenants = db.Tenants.Where(c => c.FirstName.Contains(Search) || c.LastName.Contains(Search) || c.FirstName.Contains(FN) && c.LastName.Contains(LN))
                        .Select(c => new TenantVM { ID = c.ID, FirstName = c.FirstName, LastName = c.LastName, aptID = (int)c.aptID, Phone = c.Phone, created = c.Created }).ToList();
                   }
                   else
                   {
                       mymanagement.Tenants = db.Tenants.Where(c => c.FirstName.Contains(Search) || c.LastName.Contains(Search))
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

                       Search = db.Tenants
                      .Join(db.Apartments,
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

                       Search = db.Tenants
                          .Join(db.Apartments,
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

    }
}

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
                    var mymanagement = new managementVM();

                    mymanagement.Tenants = db.Tenants.Where(c => c.FirstName.Contains(Search) || c.LastName.Contains(Search))
                        .Select(c => new  TenantVM{ID = c.ID, FirstName = c.FirstName, LastName = c.LastName,  aptID = (int)c.aptID, Phone = c.Phone, created =c.Created }).ToList();
                  




                    LoadReqType();
                    return View(mymanagement);                             
                  
                }
                 
            }
            

            return View();
        }
    }
}

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

            var todayRequests = db.Requests
                 .Where(c => c.FromDate >= Today && c.FromDate < Tomorrow)
                 .Select(c => new { Type = c.RequestType, Description = c.Description, From = c.FromDate, To = c.ToDate }).ToList();

            return new JsonResult { Data = todayRequests, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}

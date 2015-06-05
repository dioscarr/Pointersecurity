using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Doormandondemand;

namespace SecurityMonitor.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        PointerdbEntities db = new PointerdbEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        // database access
       
        public JsonResult GetGanttData()
        {
            var jsonData =  new
            {
                // create tasks array
                data =  (
                      from  t in db.GanttTask.AsEnumerable()
                    select new
                     {
                        id = t.GantTaskID,
                        text = t.Text,
                        start_date = t.StartDate.ToString("u"),
                        duration = t.Duration,
                        order = t.SortOrder,
                        progress = t.Progress,
                        open = true,
                        parent = t.ParentID,
                        type = (t.Type != null) ? t.Type : String.Empty
                    }
                ).ToArray(),
                // create links array
                links = (
                    from l in db.GanttLinkId.AsEnumerable()
                    select new
                    {
                        id = l.GantLinkID,
                        source = l.SourceTaskId,
                        target = l.TargerTaskId,
                        type = l.Type
                    }
                ).ToArray()
            };
            return new  JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GlobalSearchBuilding(string searchGobal)
      {
          var Bresult = db.Buildings.Where(c => c.Address.Contains(searchGobal) ||
              c.Zipcode.Contains(searchGobal) ||
              c.BuildingName.Contains(searchGobal) ||
              c.State.Contains(searchGobal) ||
              c.BuildingPhone.Contains(searchGobal) ||
              c.City.Contains(searchGobal)).Select(c => new { 
              BID =c.ID,
              BClientID = c.Clients.ID,
              BName = c.BuildingName,
              BAddress =c.Address,
              BCity = c.City,
              BState =c.State,
              BZipcode =c.Zipcode,
              BPhone =c.BuildingPhone             
              }).Take(10).ToList();

          var mydata = Json(Bresult);
          return new JsonResult { Data = mydata, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
      }
        public JsonResult GlobalTenantSearch(string SearechTanantGlobal)
        {
            var OBJGT = db.Tenant.Where(c => c.FirstName.Contains(SearechTanantGlobal) || 
                c.LastName.Contains(SearechTanantGlobal) ||
                c.Phone.Contains(SearechTanantGlobal) ||
                c.Username.Contains(SearechTanantGlobal))
                .Select(c => new { 
                FullName = c.FirstName +""+c.LastName,
                Email = c.Username,
                Phone=c.Phone,
                ID = c.ID,
                AptID =c.Apartment.ID,
                BID = c.Apartment.Buildings.ID

                }).Take(10).ToList();
            var data = Json(OBJGT);

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
       }
        

       
    }
}
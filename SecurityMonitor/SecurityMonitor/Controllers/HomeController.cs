using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SecurityMonitor.Models.EntityFrameworkFL;

namespace SecurityMonitor.Controllers
{
    public class HomeController : Controller
    {
        PointersecurityEntities1 db = new PointersecurityEntities1();
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
                      from  t in db.GanttTasks.AsEnumerable()
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
                    from l in db.GanttLinkIds.AsEnumerable()
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
    }
}
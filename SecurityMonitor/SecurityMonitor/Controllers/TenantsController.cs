using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Doormandondemand;
using SecurityMonitor.Models;

namespace SecurityMonitor.Controllers
{
    [Authorize(Roles="Tenant")]
    public class TenantsController : Controller
    {
        private PointerdbEntities db = new PointerdbEntities();

        // GET: Tenants
        public async Task<ActionResult> Index()
        {
            var tenants = db.Tenant.Include(t => t.Apartment);
            return View(await tenants.ToListAsync());
        }

        // GET: Tenants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = await db.Tenant.FindAsync(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // GET: Tenants/Create
        public ActionResult Create()
        {
            ViewBag.aptID = new SelectList(db.Apartment, "ID", "ApartmentNumber");
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FirstName,LastName,Phone,Username,Password,Created,isTemPWord,aptID,LogintableID")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Tenant.Add(tenant);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.aptID = new SelectList(db.Apartment, "ID", "ApartmentNumber", tenant.aptID);
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = await db.Tenant.FindAsync(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            ViewBag.aptID = new SelectList(db.Apartment, "ID", "ApartmentNumber", tenant.aptID);
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,Phone,Username,Password,Created,isTemPWord,aptID,LogintableID")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenant).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.aptID = new SelectList(db.Apartment, "ID", "ApartmentNumber", tenant.aptID);
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = await db.Tenant.FindAsync(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tenant tenant = await db.Tenant.FindAsync(id);
            db.Tenant.Remove(tenant);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }





        public ActionResult TenantIndex(string TenantID)
        {   //TODO: Tenant Dashboard
            TenantIndexVM TIvm = new TenantIndexVM();

            return View(TIvm);
        }


        [HttpGet]
        //Tenant profile ------------------------------------------------------------------------------------
        public ActionResult TenantProfile(string UserID)
        {
            TenantVM tenantprofile = new TenantVM();
            tenantprofile.ID = UserID;
            return View(tenantprofile);
        }
        [HttpGet]
        public ActionResult Repair(string tenantID)
        {
            RepairVM repair = new RepairVM();
            repair.RepairRequest = db.RepairRequest.OrderBy(c=>c.RequestedDate).ToList();
            repair.RequestCategories = db.RepairRequestCategories.Select(r => new SelectListItem { Text = r.Categories, Value = r.Id.ToString() }).ToList();
            repair.tenant = db.Tenant.Find(tenantID);
            repair.TenantID = tenantID;
            
            

            return View(repair);
        }


        public ActionResult AddRequest(RepairRequest model)
        {
            if(model!=null)
            {
                db.RepairRequest.Add(model);
                db.SaveChanges();


                var repairRequest = db.RepairRequest
                   .Where(c => c.TenantID == model.TenantID)
                   .Select(c => new
                   {
                       RequestedDate = c.RequestedDate,
                       ProblemDescription = c.ProblemDescription,
                       Status = c.Status
                   }).OrderBy(c => c.RequestedDate).ToList();


               var Jsonpackages = Json(repairRequest);


                var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
                hubContext.Clients.All.newRepairRequestList(Jsonpackages);

            }
            return View();
        }

        [HttpGet]
        public void loadRequest(string TenantID)
        {
            var repairRequest = db.RepairRequest
                     .Where(c => c.TenantID == TenantID)
                     .Select(c => new
                     {
                         RequestedDate = c.RequestedDate,
                         ProblemDescription = c.ProblemDescription,
                         Status = c.Status
                     }).OrderBy(c => c.RequestedDate).ToList();


            var Jsonpackages = Json(repairRequest);


            var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
            hubContext.Clients.All.newRepairRequestList(Jsonpackages);
          
        
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

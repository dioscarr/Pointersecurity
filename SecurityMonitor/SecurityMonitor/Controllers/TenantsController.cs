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

            var T = db.Tenant.Find(TenantID);
            var A = db.Apartment.Find(T.Apartment.ID);
            var B = db.Buildings.Find(A.Buildings.ID);
            var M = db.Module.Where(c => c.BuildingID == B.ID).ToList();

            var FT = new TenantFrontEnd();
            FT.tenant = T;
            FT.apartment = A;
            FT.building = B;
            FT.modules = M;
            //TenantIndexVM TIvm = new TenantIndexVM();
            return View(FT);
        }


        [HttpGet]
        //Tenant profile ------------------------------------------------------------------------------------
        public ActionResult TenantProfile(string UserID)
        {

            var T = db.Tenant.Find(UserID);
            var A = db.Apartment.Find(T.Apartment.ID);
            var B = db.Buildings.Find(A.Buildings.ID);
            var M = db.Module.Where(c => c.BuildingID == B.ID).ToList();

            var FT = new TenantFrontEnd();
            FT.tenant = T;
            FT.apartment = A;
            FT.building = B;
            FT.modules = M;
            return View(FT);
        }
        [HttpGet]
        public async Task<ActionResult> Repair(string tenantID)
        {
            RepairVM repair = new RepairVM();
            repair.RepairRequest = await db.RepairRequest.OrderBy(c=>c.RequestedDate).ToListAsync();
            repair.RequestCategories = await  db.RepairRequestCategories.Select(r => new SelectListItem { Text = r.Categories, Value = r.Id.ToString() }).ToListAsync();
            repair.tenant = await db.Tenant.FindAsync(tenantID);
            repair.TenantID = tenantID;
            
            

            return View(repair);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> AddRequest(RepairRequest model)
        {
            if(model!=null)
            {
                // RequestNumber Starts
                var ourdate = DateTime.Today.Date.Month.ToString() + "/" + DateTime.Today.Date.Day.ToString() + "/" + DateTime.Today.Date.Year;
                var rrc = db.repairrequestocount.OrderByDescending(c => c.Id).FirstOrDefault();
                repairrequestocount count = new repairrequestocount();

                if (rrc == null)
                {
                    count.Date = ourdate;
                    db.repairrequestocount.Add(count);
                }
                else if (rrc.Date == ourdate && rrc != null)
                {
                    count.Date = ourdate;
                    db.repairrequestocount.Add(count);
                }
                else if (rrc.Date != ourdate)
                {
                    db.Database.ExecuteSqlCommand("truncate table dbo.[repairrequestocount]");
                    count.Date = ourdate;
                    db.repairrequestocount.Add(count);
                }
                db.SaveChanges();
                var requestNumber = DateTime.Today.Date.Month.ToString() + DateTime.Today.Date.Day.ToString() + DateTime.Today.Date.Year + "-" + count.Id;
                //RequestNumber Ends

                model.RequestNumber = requestNumber;
                model.AssignID = null;
                model.AssignContractorID = null;
                db.RepairRequest.Add(model);
               await db.SaveChangesAsync();

            }

            var repairRequest = await db.RepairRequest
                .Where(c => c.TenantID == model.TenantID)
                .Select(c => new
                {
                    RequestedDate = c.RequestedDate,
                    ProblemDescription = c.ProblemDescription,
                    Status = c.Status,
                    ID = c.Id,
                    RequestNumber = c.RequestNumber,
                    Category = c.RepairRequestCategories.Categories,
                    Instruction = c.Instructions_,
                    Urgency = c.RepairUrgency.Urgency,
                    Permision =c.Permissiontoenter,
                    imgUrl = c.PhotoUrl,
                    PrimaryName = c.Tenant.FirstName +" " + c.Tenant.LastName,
                    PrimaryPhone = c.Tenant.Phone,
                    PrimaryEmail = c.Tenant.Username,
                    SecondaryName = c.OtherContactName,
                    SecondaryPhone = c.OtherContactPhone,
                    SecondaryEmail = c.OtherContactEmail,
                    BuildingID = c.BuildingID
                }).OrderBy(c => c.RequestedDate).ToListAsync();


            var Jsonpackages = Json(repairRequest);
            return new JsonResult { Data = Jsonpackages, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> loadRequest(string TenantID)
        {
            var repairRequest =await  db.RepairRequest
                     .Where(c => c.TenantID == TenantID && c.Status!="Close")
                     .Select(c => new
                     {
                         RequestedDate = c.RequestedDate,
                         ProblemDescription = c.ProblemDescription,
                         Status = c.Status,
                         ID = c.Id,
                         RequestNumber = c.RequestNumber,
                         Category = c.RepairRequestCategories.Categories,
                         Instruction = c.Instructions_,
                         Urgency = c.RepairUrgency.Urgency,
                         Permision = c.Permissiontoenter,
                         imgUrl = c.PhotoUrl,
                         PrimaryName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                         PrimaryPhone = c.Tenant.Phone,
                         PrimaryEmail = c.Tenant.Username,
                         SecondaryName = c.OtherContactName,
                         SecondaryPhone = c.OtherContactPhone,
                         SecondaryEmail = c.OtherContactEmail,
                         BuildingID = c.BuildingID
                     }).OrderBy(c => c.RequestedDate).ToListAsync();


            var Jsonpackages = Json(repairRequest);


            return new JsonResult { Data = Jsonpackages, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        
        
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> loadRequestbysearch(string TenantID, string filter)
        {
            var repairRequest = await db.RepairRequest
                     .Where(c => c.TenantID == TenantID && c.Status != "Close" && c.ProblemDescription.Contains(filter))                   
                     .Select(c => new
                     {
                         RequestedDate = c.RequestedDate,
                         ProblemDescription = c.ProblemDescription,
                         Status = c.Status,
                         ID = c.Id,
                         RequestNumber = c.RequestNumber,
                         Category = c.RepairRequestCategories.Categories,
                         Instruction = c.Instructions_,
                         Urgency = c.RepairUrgency.Urgency,
                         Permision = c.Permissiontoenter,
                         imgUrl = c.PhotoUrl,
                         PrimaryName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                         PrimaryPhone = c.Tenant.Phone,
                         PrimaryEmail = c.Tenant.Username,
                         SecondaryName = c.OtherContactName,
                         SecondaryPhone = c.OtherContactPhone,
                         SecondaryEmail = c.OtherContactEmail,
                         BuildingID = c.BuildingID
                     }).OrderByDescending(c => c.RequestedDate).ToListAsync();
            
            var Jsonpackages = Json(repairRequest);

            return new JsonResult { Data = Jsonpackages, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

         [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> UpdateRequest(RepairRequest model)
        {
            if(model!=null)
            {

                db.RepairRequest.Attach(model);
                var Entry = db.Entry(model);
                if (model.RepairRequestCategoriesID != 0)
                {
                    Entry.Property(r => r.RepairRequestCategoriesID).IsModified = true;
                }
                else if (model.Instructions_!="")
                {
                    Entry.Property(r => r.Instructions_).IsModified = true;
                }
                else if (model.UrgencyID!=0)
                {
                    Entry.Property(r => r.UrgencyID).IsModified = true;                
                }
                else if (model.Permissiontoenter!="")
                {
                    Entry.Property(r => r.Permissiontoenter).IsModified = true;
                }
                else if (model.ProblemDescription!="")
                {
                    Entry.Property(r => r.ProblemDescription).IsModified = true;                
                }
                else if (model.OtherContactName!="")
                {
                 Entry.Property(r => r.OtherContactName).IsModified = true;
                }
                else if (model.OtherContactPhone!="")
                {
                     Entry.Property(r => r.OtherContactPhone).IsModified = true;                
                }
                else if (model.OtherContactEmail!="")
                {
                    Entry.Property(r => r.OtherContactEmail).IsModified = true;                           
                }            
                
               await db.SaveChangesAsync();

            }

            var repairRequest = await db.RepairRequest
                .Where(c => c.TenantID == model.TenantID && c.Status != "Close")
                .Select(c => new
                {
                    RequestedDate = c.RequestedDate,
                    ProblemDescription = c.ProblemDescription,
                    Status = c.Status,
                    ID = c.Id,
                    RequestNumber = c.RequestNumber,
                    Category = c.RepairRequestCategories.Categories,
                    Instruction = c.Instructions_,
                    Urgency = c.RepairUrgency.Urgency,
                    Permision =c.Permissiontoenter,
                    imgUrl = c.PhotoUrl,
                    PrimaryName = c.Tenant.FirstName +" " + c.Tenant.LastName,
                    PrimaryPhone = c.Tenant.Phone,
                    PrimaryEmail = c.Tenant.Username,
                    SecondaryName = c.OtherContactName,
                    SecondaryPhone = c.OtherContactPhone,
                    SecondaryEmail = c.OtherContactEmail
                }).OrderBy(c => c.RequestedDate).ToListAsync();


            var Jsonpackages = Json(repairRequest);
            return new JsonResult { Data = Jsonpackages, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


         public JsonResult LoadCloseRequests(string TenantID)
         {


             RepairManagement OBJRM = new RepairManagement();

             var ListOfCloseRequests = OBJRM.LoadAllCloseRequestTenant(TenantID);

             var JSONdATA = Json(ListOfCloseRequests);
             return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         }

         public JsonResult TenantNotification(string TenantID)
         {


             RepairManagement OBJRM = new RepairManagement();

             var ListOfCloseRequests = OBJRM.LoadNotification(TenantID);

             var JSONdATA = Json(ListOfCloseRequests);
             return new JsonResult { Data = JSONdATA, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

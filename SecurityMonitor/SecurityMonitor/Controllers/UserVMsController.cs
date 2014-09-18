using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SecurityMonitor.Models;


namespace SecurityMonitor.Controllers
{
    public class UserVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: UserVMs
        public ActionResult Index()
        {

            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");


            if (!Roles.IsUserInRole("ctoonzofficial@gmail.com", "Admin"))
                Roles.AddUserToRole("ctoonzofficial@gmail.com", "Admin");
            //Roles.Enabled = true;
           
            return View(db.UserVMs.ToList());
        }

        // GET: UserVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVM userVM = db.UserVMs.Find(id);
            if (userVM == null)
            {
                return HttpNotFound();
            }
            return View(userVM);
        }

        // GET: UserVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,Phone,Address,City,State,Zipcode,Username,Password,created,LastActivity,isTempPWord")] UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                db.UserVMs.Add(userVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userVM);
        }

        // GET: UserVMs/Edit/5
         [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVM userVM = db.UserVMs.Find(id);
            if (userVM == null)
            {
                return HttpNotFound();
            }
            return View(userVM);
        }

        // POST: UserVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Phone,Address,City,State,Zipcode,Username,Password,created,LastActivity,isTempPWord")] UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        // GET: UserVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVM userVM = db.UserVMs.Find(id);
            if (userVM == null)
            {
                return HttpNotFound();
            }
            return View(userVM);
        }

        // POST: UserVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserVM userVM = db.UserVMs.Find(id);
            db.UserVMs.Remove(userVM);
            db.SaveChanges();
            return RedirectToAction("Index");
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

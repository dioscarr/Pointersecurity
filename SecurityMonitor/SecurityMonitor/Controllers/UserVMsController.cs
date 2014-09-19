using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SecurityMonitor.Models.EntityFrameworkFL;
using SecurityMonitor.Models;


namespace SecurityMonitor.Controllers
{
    public class UserVMsController : Controller
    {
        private PointersecurityEntities db = new PointersecurityEntities();
        
        // GET: UserVMs
        public ActionResult UserList()
        {


            var ListOfRoles = db.AspNetRoles
                .Select(r => new { Roles = r.Name, Value = r.Id}).ToList();
            ViewBag.ListOfRoles = new SelectList(ListOfRoles, "Value", "Roles");


            //Create a Role 
            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");

            //Assign a Role
            if (!Roles.IsUserInRole("ctoonzofficial@gmail.com", "Admin"))
                Roles.AddUserToRole("ctoonzofficial@gmail.com", "Admin");
            
            //Roles.Enabled = true;

            return View(db.AspNetUsers.AsEnumerable());
        }

       
    }
}

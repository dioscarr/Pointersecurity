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
using Microsoft.AspNet.Identity;


namespace SecurityMonitor.Controllers
{
    public class UserVMsController : Controller
    {
        private PointersecurityEntities1 db = new PointersecurityEntities1();
        
        // GET: UserVMs
        public ActionResult UserList()
        {
            var currentUserID = User.Identity.GetUserId();


           
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

        //[ValidateAntiForgeryToken]
        
        public ActionResult UpdateRole(string UserID, string RoleID)
        {

          
            //======save roles assigned role========
            var myRole = new Role
           {
               UserID = UserID,
               RoleID = RoleID
           };
            if (!db.Roles.ToList().Exists(exists => exists.UserID == myRole.UserID
                && exists.RoleID == myRole.RoleID))
            {
                if (ModelState.IsValid)
                {
                    db.Roles.Add(myRole);
                    db.SaveChanges();
                }

                //Join table and select needed fields
                var AssignedRoleReturn = db.Roles
                    .Join(db.AspNetRoles,
                    AssignedRoleTable => AssignedRoleTable.RoleID,
                    RoleTable => RoleTable.Id,
                    (AssignedRole, RoleTable) => new { ReturnedUserID = AssignedRole.UserID, ReturnedAssignedRoleName = RoleTable.Name })

                    .Where(n => n.ReturnedUserID == UserID);

                //========return a json selectlist=========
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(new SelectList(AssignedRoleReturn, "ReturnedUserID", " ReturnedAssignedRoleName"), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                //Join table and select needed fields
                var AssignedRoleReturn = db.Roles
                    .Join(db.AspNetRoles,
                    AssignedRoleTable => AssignedRoleTable.RoleID,
                    RoleTable => RoleTable.Id,
                    (AssignedRole, RoleTable) => new { ReturnedUserID = AssignedRole.UserID, ReturnedAssignedRoleName = RoleTable.Name })

                    .Where(n => n.ReturnedUserID == UserID);
                   
              

                //========return a json selectlist=========
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(new SelectList(AssignedRoleReturn, "ReturnedUserID", " ReturnedAssignedRoleName"), JsonRequestBehavior.AllowGet);
                }

            }

    
                        return View();
}

       
    }
}

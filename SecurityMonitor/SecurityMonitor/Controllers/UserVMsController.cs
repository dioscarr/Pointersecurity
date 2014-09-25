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
using PagedList;
using PagedList.Mvc;


namespace SecurityMonitor.Controllers
{
    public class UserVMsController : Controller
    {
        private PointersecurityEntities1 db = new PointersecurityEntities1();
        
        //============================ GET: UserVMs============================
        [HttpGet]
        public ActionResult UserList(string sortOrder)
        {
            var currentUserID = User.Identity.GetUserId();

           



           //======================= ADD USER TO ROLE ========
            var ListOfRoles = db.AspNetRoles
                .Select(r => new { Roles = r.Name, Value = r.Id}).ToList();
            ViewBag.ListOfRoles = new SelectList(ListOfRoles, "Value", "Roles");


            ////Create a Role 
            //if (!Roles.RoleExists("Admin"))
            //    Roles.CreateRole("Admin");

            ////Assign a Role
            //if (!Roles.IsUserInRole("ctoonzofficial@gmail.com", "Admin"))
            //    Roles.AddUserToRole("ctoonzofficial@gmail.com", "Admin");
            
            //=================user activity===============================

            if (User.Identity.IsAuthenticated !=false)
            {
                //====SORTING SECTION======
                ViewBag.FunctionSortParm = String.IsNullOrEmpty(sortOrder) ? "function_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                string myUserID = User.Identity.GetUserId().ToString();
                var myUserActivitiesLogVM = new UserActivityLogVM();

                myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                           .Where(UAL => UAL.UserID == myUserID)                         
                           .Select(UAL => new ActivityLog
                           {
                               UserID = UAL.UserID,
                               ID = UAL.ID,
                               DateCreated = UAL.DateOfEvent,
                               FunctionPerformed = UAL.Function_Performed,
                               Message = UAL.Message
                           }).ToList();
                ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM;

                switch (sortOrder)
                {
                    case "date_desc":
                        myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                            .Where(UAL => UAL.UserID == myUserID)
                            .OrderBy(UAL => UAL.DateOfEvent)
                            .Select(UAL => new ActivityLog
                            {
                                UserID = UAL.UserID,
                                ID = UAL.ID,
                                DateCreated = UAL.DateOfEvent,
                                FunctionPerformed = UAL.Function_Performed,
                                Message = UAL.Message
                            }).ToList();

                        ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM;
                        break;
                    case "function_desc":
                        myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                            .Where(UAL => UAL.UserID == myUserID)
                            .OrderBy(UAL => UAL.Function_Performed)
                            .Select(UAL => new ActivityLog
                            {
                                UserID = UAL.UserID,
                                ID = UAL.ID,
                                DateCreated = UAL.DateOfEvent,
                                FunctionPerformed = UAL.Function_Performed,
                                Message = UAL.Message
                            }).ToList();
                       // myUserActivitiesLogVM.UserActivites = myUserActivitiesLogVM.UserActivites.OrderBy(a => a.FunctionPerformed).ToList();
                        ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM;
                        break;

                }
               
                return View(db.AspNetUsers.AsEnumerable());

            }
          return Redirect("Home/Index");
        }
        //============================END=====================================


      
      
        //=====================UPDATE ROLE SECTION ================================
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
     
        //==============================End ==========================


        //==========================Activity partial view ==============

        public ActionResult Userprofile(int ? page) 
        {
            var currentUserID = User.Identity.GetUserId();
            if (Request.HttpMethod != "GET")
            {
                page = 1; // after post reset page to 1

            }
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            //=================user activity===============================
            var myUserActivitiesLogVM = new UserActivityLogVM();
            if (User.Identity.IsAuthenticated != false)
            {
                string myUserID = User.Identity.GetUserId().ToString();
                

                myUserActivitiesLogVM.UserActivites = db.UserActivityLogs
                           .Where(UAL => UAL.UserID == myUserID)
                           .Select(UAL => new ActivityLog
                           {
                               UserID = UAL.UserID,
                               ID = UAL.ID,
                               DateCreated = UAL.DateOfEvent,
                               FunctionPerformed = UAL.Function_Performed,
                               Message = UAL.Message
                           }).ToList();


                ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM; 
            }

            return PartialView(myUserActivitiesLogVM.UserActivites.ToPagedList(pageNumber, pageSize));
        }

        
        
        
        
        
        
        
        
        public PartialViewResult ActivityPartial()
        {


            return PartialView();
        }

     }   
}

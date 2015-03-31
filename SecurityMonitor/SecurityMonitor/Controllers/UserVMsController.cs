using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Doormandondemand;
using SecurityMonitor.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;


namespace SecurityMonitor.Controllers
{
    public class UserVMsController : Controller
    {
        private PointerdbEntities db = new PointerdbEntities();
        
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

                myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                           .Where(UAL => UAL.UserID == myUserID)                         
                           .Select(UAL => new ActivityLog
                           {
                               UserID = UAL.UserID,
                               ID = UAL.ID,
                               DateCreated = UAL.DateOfEvent,
                               FunctionPerformed = UAL.FunctionPerformed,
                               Message = UAL.Message
                           }).ToList();
                ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM;

                switch (sortOrder)
                {
                    case "date_desc":
                        myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                            .Where(UAL => UAL.UserID == myUserID)
                            .OrderBy(UAL => UAL.DateOfEvent)
                            .Select(UAL => new ActivityLog
                            {
                                UserID = UAL.UserID,
                                ID = UAL.ID,
                                DateCreated = UAL.DateOfEvent,
                                FunctionPerformed = UAL.FunctionPerformed,
                                Message = UAL.Message
                            }).ToList();

                        ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM;
                        break;
                    case "function_desc":
                        myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                            .Where(UAL => UAL.UserID == myUserID)
                            .OrderBy(UAL => UAL.FunctionPerformed)
                            .Select(UAL => new ActivityLog
                            {
                                UserID = UAL.UserID,
                                ID = UAL.ID,
                                DateCreated = UAL.DateOfEvent,
                                FunctionPerformed = UAL.FunctionPerformed,
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
            if (!db.Role.ToList().Exists(exists => exists.UserID == myRole.UserID
                && exists.RoleID == myRole.RoleID))
            {
                if (ModelState.IsValid)
                {
                    db.Role.Add(myRole);
                    db.SaveChanges();
                }

                //Join table and select needed fields
                var AssignedRoleReturn = db.Role
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
                var AssignedRoleReturn = db.Role
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

        public ActionResult Userprofile(int ? page, string searchBy, string search, int? BuildingID ) 
        {
            var currentUserID = User.Identity.GetUserId();
            if (Request.HttpMethod != "GET")
            {
                page = 1; // after post reset page to 1

            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            //=================user activity===============================
            var myUserActivitiesLogVM = new UserActivityLogVM();
            if (User.Identity.IsAuthenticated != false)
            {
                string myUserID = User.Identity.GetUserId().ToString();
                if (page == null && searchBy != null && search != null)
                {
                    ViewBag.searchBy = searchBy;
                    ViewBag.search = search;
                }
                if (page != null && searchBy != null && search != null)
                {
                    ViewBag.searchBy = searchBy;
                    ViewBag.search = search;
                }


                //=============Search=================
                if (searchBy == "Function" )
                {
                    myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                               .Where(UAL => UAL.UserID == myUserID && UAL.FunctionPerformed.Contains(search) || search==null)
                               .Select(UAL => new ActivityLog
                               {
                                   UserID = UAL.UserID,
                                   ID = UAL.ID,
                                   DateCreated = UAL.DateOfEvent,
                                   FunctionPerformed = UAL.FunctionPerformed,
                                   Message = UAL.Message
                               }).ToList();

                }

                else if (searchBy == "Date")
                {
                    DateTime myvar = new DateTime();
                    if (DateTime.TryParse(search, out myvar))
                    {
                        string actualdate = search.Substring(0, 10);
                        DateTime theTime = Convert.ToDateTime(actualdate);
                        myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                                   .Where(UAL => UAL.UserID == myUserID && UAL.DateOfEvent == theTime)
                                   .Select(UAL => new ActivityLog
                                   {
                                       UserID = UAL.UserID,
                                       ID = UAL.ID,
                                       DateCreated = UAL.DateOfEvent,
                                       FunctionPerformed = UAL.FunctionPerformed,
                                       Message = UAL.Message
                                   }).ToList();
                    }
                    else 
                    {
                        ViewBag.ItisNotaDay = search;

                        myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                             .Where(UAL => UAL.UserID == myUserID)
                             .Select(UAL => new ActivityLog
                             {
                                 UserID = UAL.UserID,
                                 ID = UAL.ID,
                                 DateCreated = UAL.DateOfEvent,
                                 FunctionPerformed = UAL.FunctionPerformed,
                                 Message = UAL.Message,

                             }).ToList();
                    }
                }
                else
                {
                    myUserActivitiesLogVM.UserActivites = db.UserActivityLog
                              .Where(UAL => UAL.UserID == myUserID)
                              .Select(UAL => new ActivityLog
                              {
                                  UserID = UAL.UserID,
                                  ID = UAL.ID,
                                  DateCreated = UAL.DateOfEvent,
                                  FunctionPerformed = UAL.FunctionPerformed,
                                  Message = UAL.Message,

                              }).ToList();
                }
                ViewBag.myUserActivitiesLogVM = myUserActivitiesLogVM; 
            }

            if (BuildingID !=null)
            {
                Session["BuildingID"] = BuildingID;
            }
            else
            {
                BuildingID = (int)Session["BuildingID"];
            }
          var buildinginfo = db.Buildings
                .Where(c => c.ID == BuildingID)
                .Select(c => new BuildingInfoVM {
                    ID = c.ID,
                    BuildingName = c.BuildingName,
                    BuildingPhone = c.BuildingPhone,
                    Address =c.Address,
                    City = c.City,
                    ZipCode = c.Zipcode,
                    Manager = c.Manager,
                    NumberOfApart = (int)c.NumberOfApartment,
                    States = c.State
                }).First();
            
            ViewBag.buildingInfo = buildinginfo;


            return PartialView(myUserActivitiesLogVM.UserActivites.ToPagedList(pageNumber, pageSize));
        }
        //TODO: check op this action. this might not be in use.
       
        //==================Building Information================
      

     }   
}

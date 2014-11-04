using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SecurityMonitor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
          
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("UserVMs",
               "UserVMs/UpdateRole/{UserID}/{RoleID}",
               new { controller = "UserVMs", action = "UpdateRole", UserID = "", RoleID="" });

            routes.MapRoute("Userprofile",
             "UserVMs/Userprofile/{sortOrder}",
             new { controller = "UserVMs", action = "Userprofile",  sortOrder = "" });


            routes.MapRoute(
              name: "ManageUseslist",
              url: "{userVMs}/{UserList}/{id}",
              defaults: new { controller = "UserVMs", action = "UserList", id = UrlParameter.Optional }
          );

            routes.MapRoute("AddApartment",
           "Building/AddApartment/{buildingID}",
           new { controller = "Building", action = "AddApartment", buildingID = "" });
           
            routes.MapRoute("ApartmentPfrofile",
       "Building/ApartmentProfile/{ApartmentID}",
       new { controller = "Building", action = "ApartmentProfile", ApartmentID = "" });

            routes.MapRoute("getTodayRequests",
          "{controller}/{action}",
       new { controller = "DashBoard", action = "getTodayRequests" });

         
            
        }
    }
}

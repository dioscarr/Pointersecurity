using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace SecurityMonitor.Models
{
    public class HandleRole
    {
        ApplicationDbContext context = new ApplicationDbContext();

        private string InsertRoledb(string RoleName)
        {
            var result = "";
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!RoleManager.RoleExists(RoleName))
            {
                var roleresult = RoleManager.Create(new IdentityRole(RoleName));
                result = "Successful";
            }
            else { result = "It doesn't exist"; }
            return result;
        }
        private string DeleteRoledb(string RoleName)
        {
            var result = "";
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (RoleManager.RoleExists(RoleName))
            {
                var roleresult = RoleManager.Delete(new IdentityRole(RoleName));
                result = "Successful";
            }
            else { result = "It doesn't exist"; }
            return result;
        }
            

    }
}
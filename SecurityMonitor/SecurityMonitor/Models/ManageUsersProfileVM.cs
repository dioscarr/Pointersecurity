using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PointerSecurityAzure;

namespace SecurityMonitor.Models
{
    public class ManageUsersProfileVM
    {
        pointersecurityEntities db = new pointersecurityEntities();
        ApplicationDbContext context = new ApplicationDbContext();
        public int ID { get; set; }
        public string RoleName { get; set; }
        public int BuildingID { get; set; }
        public int UserID { get; set; }
        public ManagerVM managerVM{ get; set; }
        public Permission permission { get; set; }
        



        public string InsertRole(string RoleName)
        {
            string results = InsertRoledb(RoleName);
            return results;
        }
        public string CreatUser(ManageUsersProfileVM model)
        {
            var result = CreatUserdb(model);
            return result.ToString();
        }
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
            else { result= "It doesn't exist"; }
            return result;
        }
        private async Task<string> CreatUserdb(ManageUsersProfileVM modelManagementVM)
        {

            try
            {
                
                ApplicationDbContext context = new ApplicationDbContext();

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                PasswordHasher hasher = new PasswordHasher();
                var a = UserManager.FindByEmail(modelManagementVM.managerVM.Email);
                if (a != null)
                {
                    return "";
                }
                ApplicationUser AppUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = modelManagementVM.managerVM.Email,
                    UserName = modelManagementVM.managerVM.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = modelManagementVM.managerVM.Phone,
                    LockoutEnabled = false,
                    LockoutEndDateUtc = DateTime.Now.AddDays(365),
                    AccessFailedCount = 0,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(modelManagementVM.managerVM.Password)
                };
                string[] FullName = modelManagementVM.managerVM.FullName.Split(new string[] { " " }, StringSplitOptions.None);
                Manager mgr = new Manager()
                {
                    ID = AppUser.Id,
                    FirstName = FullName[0].ToString(),
                    LastName = FullName[1].ToString(),
                    Phone = modelManagementVM.managerVM.Phone,
                    ClientID = modelManagementVM.managerVM.clientID
                };
                PermissionMapRole ObjPerm = new PermissionMapRole() {
                    UserID = AppUser.Id,
                    //RoleID =  TODO!!!!      
                };
                

                db.Manager.Add(mgr);
                context.Users.Add(AppUser);

                await context.SaveChangesAsync();
                await db.SaveChangesAsync();    

                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!RoleManager.RoleExists("Manager"))
                { var roleresult = RoleManager.Create(new IdentityRole("Manager")); }
                var Result = UserManager.AddToRole(AppUser.Id, "Manager");

                ManagerBuilding ObjManagerBuilding = new ManagerBuilding()
                {
                    BuildingID = modelManagementVM.managerVM.BuildingID,
                    ManagerID = mgr.ID,
                    UserID = mgr.ID
                };

                db.ManagerBuilding.Add(ObjManagerBuilding);
                await db.SaveChangesAsync();


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return ""; 
        }
            



    }
}
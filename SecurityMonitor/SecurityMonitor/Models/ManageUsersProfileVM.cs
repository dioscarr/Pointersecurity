using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    public class ManageUsersProfileVM
    {
        PointerdbEntities db = new PointerdbEntities();
        ApplicationDbContext context = new ApplicationDbContext();
        public int ID { get; set; }
        public List<string> RoleNames { get; set; }
        public int BuildingID { get; set; }
        public int UserID { get; set; }
        public ManagerVM managerVM { get; set; }
        public Permission permission { get; set; }

        
        public string InserUserPermission(string RoleName, string UserID)
        {
            string results = InserUserPermissiondb(RoleName, UserID);
            return results;
        }
        public string DeleteUserPermission(string RoleName, string UserID)
        {
            string results = DeleteUserPermissiondb(RoleName, UserID);
            return results;
        }
        public string InsertUser(ManageUsersProfileVM model)
        {
            var result = InsertUserdb(model);
            return result.ToString();
        }
   
          
        /// <summary>
        /// Takes a ManageUsersProfileVM as arguement which contain permission model: Return a list of RoleName that were set to true.
        /// </summary>
        /// <param name="ConvertToRoleNames"></param>
        /// <returns></returns>
        public List<string> ConvertToRoleNames(PermissionBase model)
        { 
             List<string> myRoles = new List<string> ();


             if (model.News == true)
             {
                 myRoles.Add("News");
             }
             if (model.Accesscontrol == true)
             {
                 myRoles.Add("AccessControl");
             }
             if (model.Delivery == true)
             {
                 myRoles.Add("Delivery");
             }
             if (model.Events == true)
             {
                 myRoles.Add("Events");
             }
             if (model.LegalDocs == true)
             {
                 myRoles.Add("LegalDocs");
             }
             if (model.Repair == true)
             {
                 myRoles.Add("Repair");
             }
             if (model.BasicFeatures == true)
             {
                 myRoles.Add("BasicFeatures");
             }
             if (model.Apartment == true)
             {
                 myRoles.Add("Apartment");
             }
             if (model.Contactbook == true)
             {
                 myRoles.Add("Contactbook");
             }
           

             return myRoles;
        }
        public string AddBuildingUser(ManagerVM model, string AspnetUserID)
        {
            string[] FullName = model.FullName.Split(new string[] { " " }, StringSplitOptions.None);                
           
            BuildingUser ObjBU = new BuildingUser() 
            {
             Id = AspnetUserID,
             BuildingID = model.BuildingID,
             FirstName = FullName[0].ToString(),
             LastName = FullName[1].ToString(),
             Phone = model.Phone,
             Email = model.Email,
             UserName = model.Username,
             UserID = AspnetUserID
            };
            db.BuildingUser.Add(ObjBU);
            db.SaveChanges();

            return AspnetUserID;
        }
       
        public List<BuildingUser> LoadBuildingUsers(int BuildingID)
        {
         
            List<BuildingUser> ObjBU = db.BuildingUser.Where(c=>c.BuildingID==BuildingID).ToList();
            return ObjBU;
        }
        /// <summary>
        /// remove all current permission for the given user and insert new given permissions. 
        /// </summary>
        /// <param name="newRoles">Accepts a list of RolesName type string</param>
        /// <param name="UserID">Accepts a 128 chr userID type string</param>
        /// <returns></returns>
        public async Task<string> EditbuildingUserPermission(List<string> newRoles, string UserID)
        {            
            var result = "";
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //get all roles for the current user
            List<string> AllRoles = UserManager.GetRoles(UserID).ToList();
            //delete all the roles
            foreach (var item in AllRoles)
            {
                await UserManager.RemoveFromRoleAsync(UserID, item);
            }
            //add new roles 
            foreach (var item in newRoles)
            {
                if (RoleManager.RoleExists(item))
                {
                    await UserManager.AddToRoleAsync(UserID, item);                    
                }  
            }
           
            return result;
        
        }

        /// <summary>
        /// load and return a Json Object populated with all permissions granted to the UserID pass if any exist.
        /// </summary>
        /// <param name="LoadBuildingUserPermission"></param>
        /// <returns></returns>
        public IList<string> LoadBuildingUserPermission(string BuildingUserID)
        {    
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
          var ObjBU = UserManager.GetRoles(BuildingUserID);

            return ObjBU;          
        }

        private string InserUserPermissiondb(string RoleName, string UserID)
        {
           
            var result = "";
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (RoleManager.RoleExists(RoleName))
            {
                UserManager.AddToRole(UserID, RoleName);
                result = "Successful";
            }
            else { result = "It doesn't exist"; }
            return result;
            
        }
        private string DeleteUserPermissiondb(string RoleName, string UserID)
        {
            var result = "";
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (RoleManager.RoleExists(RoleName))
            {
                UserManager.RemoveFromRole(UserID, RoleName);
                result = "Successful";
            }
            else { result= "It doesn't exist"; }
            return result;
        }
        private string InsertUserdb(ManageUsersProfileVM model)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                PasswordHasher hasher = new PasswordHasher();
                var a = UserManager.FindByEmail(model.managerVM.Email);
                if (a != null)
                {
                    return "";
                }
                ApplicationUser AppUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = model.managerVM.Email,
                    UserName = model.managerVM.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = model.managerVM.Phone,
                    LockoutEnabled = false,
                    LockoutEndDateUtc = DateTime.Now.AddDays(365),
                    AccessFailedCount = 0,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(model.managerVM.Password)
                };
                string[] FullName = model.managerVM.FullName.Split(new string[] { " " }, StringSplitOptions.None);
               
                context.Users.Add(AppUser);
                 context.SaveChangesAsync();
                

                 return AppUser.Id;

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
          
        }
            



    }
}
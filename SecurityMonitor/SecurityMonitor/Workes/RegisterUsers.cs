using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SecurityMonitor.Models;
using Doormandondemand;
using System.Threading.Tasks;


namespace SecurityMonitor.Workes
{
    public class RegisterUsers
    {

        ApplicationDbContext context = new ApplicationDbContext();
        PointerdbEntities db = new PointerdbEntities();
        public static string InserToRole(string RoleName, string UserID)
        {
            string results = InserToRoledb(RoleName, UserID);
            return results;
        }
        public string DeleteUserPermission(string RoleName, string UserID)
        {
            string results = DeleteUserPermissiondb(RoleName, UserID);
            return results;
        }
        /// <summary>
        /// Insert a new aspNetUset
        /// </summary>
        /// <param name="UserName">Accepts a string User Name/ Email.</param>
        /// <param name="UserName">Accepts a string Phone Number.</param>
        /// <param name="UserName">Accepts a string Password.</param>
        /// <returns></returns>
     
        public static string InsertUser(string UserName, string Phone, string Password)
        {
            var result = InsertUserdb(UserName, Phone, Password);
            return result.ToString();
        }       

        /// <summary>
        /// Takes a ManageUsersProfileVM as arguement which contain permission model: Return a list of RoleName that were set to true.
        /// </summary>
        /// <param name="ConvertToRoleNames"></param>
        /// <returns></returns>
        public static List<string> ConvertToRoleNames(ManageUsersProfileVM model)
        {
            List<string> myRoles = new List<string>();


            if (model.permission.permission.News == true)
            {
                myRoles.Add("News");
            }
            if (model.permission.permission.Accesscontrol == true)
            {
                myRoles.Add("AccessControl");
            }
            if (model.permission.permission.Delivery == true)
            {
                myRoles.Add("Delivery");
            }
            if (model.permission.permission.Events == true)
            {
                myRoles.Add("Events");
            }
            if (model.permission.permission.LegalDocs == true)
            {
                myRoles.Add("LegalDocs");
            }
            if (model.permission.permission.Repair == true)
            {
                myRoles.Add("Repair");
            }
            if (model.permission.permission.BasicFeatures == true)
            {
                myRoles.Add("BasicFeatures");
            }
            if (model.permission.permission.Apartment == true)
            {
                myRoles.Add("Apartment");
            }
            if (model.permission.permission.Contactbook == true)
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

            return "";
        }

        public List<BuildingUser> LoadBuildingUsers(int BuildingID)
        {

            List<BuildingUser> ObjBU = db.BuildingUser.Where(c => c.BuildingID == BuildingID).ToList();
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
        /// <summary>
        /// Accepts Rolename and an AspNetUsers ID
        /// </summary>
        /// <param name="RoleName">data type "string" role name</param>
        /// <param name="UserID">128 characters data type"string"</param>
        /// <returns></returns>
        private static string InserToRoledb(string RoleName, string UserID)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var result = "";
            var RoleManager1 = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (RoleManager1.RoleExists(RoleName))
            {
                if (UserManager.IsInRole(UserID, RoleName))
                {
                    result = "Already in role";
                
                }
                UserManager.AddToRole(UserID, RoleName);
                result = "Successful";
            }
            else if (!RoleManager1.RoleExists(RoleName)) 
            {
                var IRoleName = new IdentityRole();
                IRoleName.Name = RoleName;
                RoleManager1.Create(IRoleName);

                UserManager.AddToRole(UserID, RoleName);
                result = "Successful";
                
            }
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
            else { result = "It doesn't exist"; }
            return result;
        }
        /// <summary>
        /// Insert a new aspNetUset
        /// </summary>
        /// <param name="UserName">Accepts a string User Name/ Email.</param>
        /// <param name="UserName">Accepts a string Phone Number.</param>
        /// <param name="UserName">Accepts a string Password.</param>
        /// <returns></returns>
        private static string InsertUserdb(string UserName, string Phone, string Password)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                PasswordHasher hasher = new PasswordHasher();
                var a = UserManager.FindByEmail(UserName);
                if (a != null)
                {
                    return a.Id ;
                }
                ApplicationUser AppUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = UserName,
                    UserName = UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = Phone,
                    LockoutEnabled = false,
                    LockoutEndDateUtc = DateTime.Now.AddDays(365),
                    AccessFailedCount = 0,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    PasswordHash = hasher.HashPassword(Password)
                };              

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
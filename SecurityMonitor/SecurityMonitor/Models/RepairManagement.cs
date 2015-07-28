using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Doormandondemand;

namespace SecurityMonitor.Models
{
    

    public class RepairManagement
    {

        PointerdbEntities db = new PointerdbEntities();


        public int id { get; set; }
        public List<RepairRequestmock> RepairsRequests { get; set; }
        public int buildingID { get; set; }
        public Buildings building { get; set; }
                
        /// <summary>
        /// Loads all the records that belongs to a specific building
        /// </summary>
        /// <param name="buildingID">Accepts building ID type int</param>
        /// <returns> Returns a List all type RepairRequest</returns>
        public List<RepairRequestmock> LoadAllRequest(int buildingID)
        {
           
           var R = db.RepairRequest
                .Where(r => r.BuildingID == buildingID && r.Status != "Close" ||r.BuildingID == buildingID && r.Status == "ReOpen")
                .Select(c =>new RepairRequestmock{
                      RequestedDate = c.RequestedDate,
                      Description = c.ProblemDescription,                   
                      Status = c.Status,
                      ID = c.Id,
                      RequestNumber=c.RequestNumber,
                      Category = c.RepairRequestCategories.Categories,
                      PhotoUrl = c.PhotoUrl,
                      Urgency = c.RepairUrgency.Urgency,
                      CName = c.OtherContactName,
                      CEmail = c.OtherContactEmail,
                      CPhone = c.OtherContactPhone,
                      PName = c.Tenant.FirstName+" "+c.Tenant.LastName,
                      PEmail =c.Tenant.Username,
                      PPhone = c.Tenant.Phone,
                      AssignToID = c.BuildingUser.Id,
                      AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                      assignContractorID = c.AssignContractorID,
                      ContractorFullName = c.Contractor.CompanyName


            }).ToList();
            return R;
        }

       


        public List<RepairRequestmock> LoadAllCloseRequest(int buildingID)
        {

            var R = db.RepairRequest
                 .Where(r => r.BuildingID == buildingID && r.Status=="Close")
                 .Select(c => new RepairRequestmock
                 {
                     RequestedDate = c.RequestedDate,
                     Description = c.ProblemDescription,
                     Status = c.Status,
                     ID = c.Id,
                     RequestNumber = c.RequestNumber,
                     Category = c.RepairRequestCategories.Categories,
                     PhotoUrl = c.PhotoUrl,
                     Urgency = c.RepairUrgency.Urgency,
                     CName = c.OtherContactName,
                     CEmail = c.OtherContactEmail,
                     CPhone = c.OtherContactPhone,
                     PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                     PEmail = c.Tenant.Username,
                     PPhone = c.Tenant.Phone,
                     AssignToID = c.BuildingUser.Id,
                     AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                     assignContractorID = c.AssignContractorID,
                     ContractorFullName = c.Contractor.CompanyName


                 }).ToList();
            return R;
        }

        public string ReopenRepairTicket(int buildingID)
        {

            var model = db.RepairRequest.Find(buildingID);
            model.Status = "ReOpen";
            db.RepairRequest.Attach(model);
            var Entry = db.Entry(model);
            Entry.Property(c => c.Status).IsModified = true;
            db.SaveChanges();

            return "Sucessful";
            
        }

        public List<RepairRequestmock> SearchByRequestNumber(string filter, int BuildingID)
        {
           var R = db.RepairRequest
                 .Where(r => r.BuildingID == BuildingID && r.RequestNumber.Contains(filter) && r.Status!="Close")
                 .Select(c => new RepairRequestmock
                 {
                     RequestedDate = c.RequestedDate,
                     Description = c.ProblemDescription,
                     Status = c.Status,
                     ID = c.Id,
                     RequestNumber = c.RequestNumber,
                     Category = c.RepairRequestCategories.Categories,
                     PhotoUrl = c.PhotoUrl,
                     Urgency = c.RepairUrgency.Urgency,
                     CName = c.OtherContactName,
                     CEmail = c.OtherContactEmail,
                     CPhone = c.OtherContactPhone,
                     PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                     PEmail = c.Tenant.Username,
                     PPhone = c.Tenant.Phone,
                     AssignToID = c.BuildingUser.Id,
                     AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                     assignContractorID = c.AssignContractorID,
                     ContractorFullName = c.Contractor.CompanyName


                 }).ToList();
            return R;
        }
         
        public List<RepairRequestmock> LoadCloseRequestsbaseonsearch(int buildingID, string filterRequestNumber)
        {
            var R = db.RepairRequest
                 .Where(r => r.BuildingID == buildingID && r.Status == "Close" && r.RequestNumber.Contains(filterRequestNumber))
                 .Select(c => new RepairRequestmock
                 {
                     RequestedDate = c.RequestedDate,
                     Description = c.ProblemDescription,
                     Status = c.Status,
                     ID = c.Id,
                     RequestNumber = c.RequestNumber,
                     Category = c.RepairRequestCategories.Categories,
                     PhotoUrl = c.PhotoUrl,
                     Urgency = c.RepairUrgency.Urgency,
                     CName = c.OtherContactName,
                     CEmail = c.OtherContactEmail,
                     CPhone = c.OtherContactPhone,
                     PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                     PEmail = c.Tenant.Username,
                     PPhone = c.Tenant.Phone,
                     AssignToID = c.BuildingUser.Id,
                     AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                     assignContractorID = c.AssignContractorID,
                     ContractorFullName = c.Contractor.CompanyName


                 }).ToList();
            return R;
        }



        public List<RepairRequestmock> LoadAllRequestSortStatusASC(int buildingID)
        {
            var R = db.RepairRequest
                .Where(r => r.BuildingID == buildingID && r.Status != "Close" || r.BuildingID == buildingID && r.Status == "ReOpen")
                .OrderBy(c=>c.Status)
                .Select(c => new RepairRequestmock
                {
                    RequestedDate = c.RequestedDate,
                    Description = c.ProblemDescription,
                    Status = c.Status,
                    ID = c.Id,
                    RequestNumber = c.RequestNumber,
                    Category = c.RepairRequestCategories.Categories,
                    PhotoUrl = c.PhotoUrl,
                    Urgency = c.RepairUrgency.Urgency,
                    CName = c.OtherContactName,
                    CEmail = c.OtherContactEmail,
                    CPhone = c.OtherContactPhone,
                    PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                    PEmail = c.Tenant.Username,
                    PPhone = c.Tenant.Phone,
                    AssignToID = c.BuildingUser.Id,
                    AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                    assignContractorID = c.AssignContractorID,
                    ContractorFullName = c.Contractor.CompanyName


                }).ToList();
            return R;
        }



        public List<RepairRequestmock> LoadAllRequestSortUrgencyASC(int buildingID)
        {
            var R = db.RepairRequest
                .Where(r => r.BuildingID == buildingID && r.Status != "Close" || r.BuildingID == buildingID && r.Status == "ReOpen")
                .OrderBy(c=>c.RepairUrgency.Urgency)
                .Select(c => new RepairRequestmock
                {
                    RequestedDate = c.RequestedDate,
                    Description = c.ProblemDescription,
                    Status = c.Status,
                    ID = c.Id,
                    RequestNumber = c.RequestNumber,
                    Category = c.RepairRequestCategories.Categories,
                    PhotoUrl = c.PhotoUrl,
                    Urgency = c.RepairUrgency.Urgency,
                    CName = c.OtherContactName,
                    CEmail = c.OtherContactEmail,
                    CPhone = c.OtherContactPhone,
                    PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                    PEmail = c.Tenant.Username,
                    PPhone = c.Tenant.Phone,
                    AssignToID = c.BuildingUser.Id,
                    AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                    assignContractorID = c.AssignContractorID,
                    ContractorFullName = c.Contractor.CompanyName


                }).ToList();
            return R;
        }
        public List<RepairRequestmock> LoadAllRequestSortByDateASC(int buildingID)
        {
            var R = db.RepairRequest
                .Where(r => r.BuildingID == buildingID && r.Status != "Close" || r.BuildingID == buildingID && r.Status == "ReOpen")
                .OrderBy(c=>c.RequestedDate)
                .Select(c => new RepairRequestmock
                {
                    RequestedDate = c.RequestedDate,
                    Description = c.ProblemDescription,
                    Status = c.Status,
                    ID = c.Id,
                    RequestNumber = c.RequestNumber,
                    Category = c.RepairRequestCategories.Categories,
                    PhotoUrl = c.PhotoUrl,
                    Urgency = c.RepairUrgency.Urgency,
                    CName = c.OtherContactName,
                    CEmail = c.OtherContactEmail,
                    CPhone = c.OtherContactPhone,
                    PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                    PEmail = c.Tenant.Username,
                    PPhone = c.Tenant.Phone,
                    AssignToID = c.BuildingUser.Id,
                    AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                    assignContractorID = c.AssignContractorID,
                    ContractorFullName = c.Contractor.CompanyName


                }).ToList();
            return R;
        }
        public List<RepairRequestmock> LoadAllRequestSortRequestNumberASC(int buildingID)
        {
            var R = db.RepairRequest
                .Where(r => r.BuildingID == buildingID && r.Status != "Close" || r.BuildingID == buildingID && r.Status == "ReOpen")
                .OrderBy(c=>c.RequestNumber)
                .Select(c => new RepairRequestmock
                {
                    RequestedDate = c.RequestedDate,
                    Description = c.ProblemDescription,
                    Status = c.Status,
                    ID = c.Id,
                    RequestNumber = c.RequestNumber,
                    Category = c.RepairRequestCategories.Categories,
                    PhotoUrl = c.PhotoUrl,
                    Urgency = c.RepairUrgency.Urgency,
                    CName = c.OtherContactName,
                    CEmail = c.OtherContactEmail,
                    CPhone = c.OtherContactPhone,
                    PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                    PEmail = c.Tenant.Username,
                    PPhone = c.Tenant.Phone,
                    AssignToID = c.BuildingUser.Id,
                    AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                    assignContractorID = c.AssignContractorID,
                    ContractorFullName = c.Contractor.CompanyName


                }).ToList();
            return R;
        }

        public List<RepairRequestmock> LoadAllCloseRequestTenant(string TenantID)
        {

            var R = db.RepairRequest
                .Where(r => r.Tenant.AspNetUsers.Id == TenantID && r.Status == "Close")
                .OrderBy(c=>c.RequestNumber)
                .Select(c => new RepairRequestmock
                {
                    RequestedDate = c.RequestedDate,
                    Description = c.ProblemDescription,
                    Status = c.Status,
                    ID = c.Id,
                    RequestNumber = c.RequestNumber,
                    Category = c.RepairRequestCategories.Categories,
                    PhotoUrl = c.PhotoUrl,
                    Urgency = c.RepairUrgency.Urgency,
                    CName = c.OtherContactName,
                    CEmail = c.OtherContactEmail,
                    CPhone = c.OtherContactPhone,
                    PName = c.Tenant.FirstName + " " + c.Tenant.LastName,
                    PEmail = c.Tenant.Username,
                    PPhone = c.Tenant.Phone,
                    AssignToID = c.BuildingUser.Id,
                    AssignedFullName = c.BuildingUser.FirstName + " " + c.BuildingUser.LastName,
                    assignContractorID = c.AssignContractorID,
                    ContractorFullName = c.Contractor.CompanyName


                }).ToList();
            return R;
       

        }

       public List<TenantNotification>LoadNotification(string TenantID)
        {
            var R = db.RepairRequest
                .Where(r => r.Tenant.AspNetUsers.Id == TenantID && r.Status == "Close" && r.TenantNotified == false || r.Tenant.AspNetUsers.Id == TenantID && r.Status == "Assigned" && r.TenantNotified == false)
                .OrderByDescending(c => c.RequestedDate)
                .Select(c => new TenantNotification
                {
                    RequestedDate = c.RequestedDate.Month +"/"+c.RequestedDate.Day +"/"+c.RequestedDate.Year,
                   TicketNumber = c.RequestNumber,
                   NotificationMessage ="has been "+ c.Status

                }).ToList();
            return R;
        }
    }
}
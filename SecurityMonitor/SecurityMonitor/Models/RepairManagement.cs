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
                .Where(r => r.BuildingID == buildingID)
                .Select(c =>new RepairRequestmock{
                      RequestedDate = c.RequestedDate,
                      Description = c.ProblemDescription,                   
                      Status = c.Status,
                      ID = c.Id,
                      RequestNumber="04/23/2015",
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
    }
}
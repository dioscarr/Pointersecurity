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
                
        /// <summary>
        /// Loads all the records that belongs to a specific building
        /// </summary>
        /// <param name="buildingID">Accepts building ID type int</param>
        /// <returns> Returns a List all type RepairRequest</returns>
        public List<RepairRequestmock> LoadAllRequest(int buildingID)
        {
            List<RepairRequestmock> R = new List<RepairRequestmock>();
            R = db.RepairRequest
                .Where(r => r.Tenant.Apartment.Buildings.ID == buildingID)
                .Select(c =>new RepairRequestmock{
                     RequestedDate = c.RequestedDate,
                      Description = c.ProblemDescription,
                      ID = c.Id,
                      RequestNumber="04/23/2015",
                      Status = c.Status
            }).ToList();
            return R;
        }
    }
}
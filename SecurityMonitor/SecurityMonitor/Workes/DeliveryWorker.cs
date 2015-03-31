using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecurityMonitor.Models;
using Doormandondemand;


namespace SecurityMonitor.Workes
{
    public class DeliveryWorker
    {
        PointerdbEntities db = new PointerdbEntities();
        public BuildingUserMappingVM LoadUserBuildingID(string UserID)
        { 
            
            BuildingUserMappingVM model = new BuildingUserMappingVM();

            model = db.BuildingUserMapping
                            .Where(b => b.UserID == UserID)
                            .Select(b => new BuildingUserMappingVM 
                                              {
                                                  ID = b.ID, 
                                                  BuildingID = b.BuildingID, 
                                                  UserID=b.UserID 
                                              })
                            .FirstOrDefault();
            return model;
        
        }
        /// <summary>
        /// Add a new shipment to the db and return a fully loaded shipment
        /// </summary>
        /// <param name="modelShipment">Accepts a shipment fully populated with no id</param>
        /// <returns>Returns a loaded Shipment </returns>
        public string AddShipment(ShipmentVM s, string BuildingUserID)
        {
            try
            {

                s.ID = Guid.NewGuid().ToString();

                Shipment ObjS = new Shipment
                {
                    ID = s.ID,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Phone = s.Phone,
                    Address = s.Address,
                    City = s.City,
                    State = s.State,
                    Zipcode = s.Zipcode,
                    aptID = s.ApartmentID,
                    ApartmentNumber = s.ApartmentNumber,
                    BuildingID = s.BuildingID,
                    TenantID = s.UserID,
                    Notified = false,
                    Created = DateTime.Now,
                    BuildingUserID = BuildingUserID,
                    isNewUser =s.isNewUser                      
                };
                List<Package> ObjPs = new List<Package>();
                foreach (var item in s.Packages)
                {
                    Package ObjP = new Package
                    {
                        ArrivalTime = DateTime.Now,
                        Note = item.Note,
                        TrackingNumber = item.Trackingnumber,
                        ShipmentID = ObjS.ID,
                        ShippingCarrierID = Convert.ToInt32(item.Service),
                        ShippingServiceID = Convert.ToInt32(item.shippingService),
                        PackageDeliveryStatusID = 1,// default status Undelivered
                        PakageTypeID = Convert.ToInt32(item.PackageType)
                    };

                    ObjPs.Add(ObjP);
                };
       

                db.Shipment.Add(ObjS);
             
                db.Package.AddRange(ObjPs);

                db.SaveChanges();
            
                return "Shipment was successfully added!";
            }
            catch (Exception)
            {
                throw;
            }                    
        }
        /// <summary>
        /// Return a fully populated shipment
        /// </summary>
        /// <param name="ShipmentID"></param>
        /// <returns></returns>
        public Shipment LoadShipment(string ShipmentID)
        {
            try
            {
                var shipment = db.Shipment.Find(ShipmentID);
                return shipment;
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public void AddingPackage(Package modelPackage)
        { 
                    
        }
    }
}
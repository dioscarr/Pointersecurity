﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PointerSecurityDataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NewPointerdbEntities : DbContext
    {
        public NewPointerdbEntities()
            : base("name=NewPointerdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActiveManager> ActiveManager { get; set; }
        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Buildings> Buildings { get; set; }
        public virtual DbSet<BuildingUser> BuildingUser { get; set; }
        public virtual DbSet<BuildingUserMapping> BuildingUserMapping { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<GanttLinkId> GanttLinkId { get; set; }
        public virtual DbSet<GanttTask> GanttTask { get; set; }
        public virtual DbSet<ListOfModule> ListOfModule { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<ManagerBuilding> ManagerBuilding { get; set; }
        public virtual DbSet<MasterProfileFields> MasterProfileFields { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<PackageDeliveryStatus> PackageDeliveryStatus { get; set; }
        public virtual DbSet<PendingModules> PendingModules { get; set; }
        public virtual DbSet<PermissionMapRole> PermissionMapRole { get; set; }
        public virtual DbSet<ReqType> ReqType { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<ShippingCarrier> ShippingCarrier { get; set; }
        public virtual DbSet<ShippingService> ShippingService { get; set; }
        public virtual DbSet<SignalRMessageTable> SignalRMessageTable { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tenant> Tenant { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLog { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageType> PackageType { get; set; }
    }
}

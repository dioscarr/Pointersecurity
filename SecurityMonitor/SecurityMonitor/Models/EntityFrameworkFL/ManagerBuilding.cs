//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecurityMonitor.Models.EntityFrameworkFL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ManagerBuilding
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public string UserID { get; set; }
        public string ManagerID { get; set; }
    
        public virtual Buildings Buildings { get; set; }
        public virtual Manager Manager { get; set; }
    }
}

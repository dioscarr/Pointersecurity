//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Doormandondemand
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackageType
    {
        public PackageType()
        {
            this.Package = new HashSet<Package>();
        }
    
        public int ID { get; set; }
        public string PackageType1 { get; set; }
    
        public virtual ICollection<Package> Package { get; set; }
    }
}

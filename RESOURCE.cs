//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SurvivorApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class RESOURCE
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Nullable<int> SurvivorId { get; set; }
    
        public virtual SURVIVOR SURVIVOR { get; set; }
    }
}

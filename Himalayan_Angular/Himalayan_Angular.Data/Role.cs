//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Himalayan_Angular.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public Nullable<bool> Optional { get; set; }
    
        public virtual RoleEntity RoleEntity { get; set; }
        public virtual User User { get; set; }
    }
}

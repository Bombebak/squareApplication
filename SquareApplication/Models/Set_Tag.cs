//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SquareApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Set_Tag
    {
        public Nullable<int> set_id { get; set; }
        public Nullable<int> tag_id { get; set; }
        public int settag_id { get; set; }
    
        public virtual Set Set { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToDo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class List
    {
        public int ListID { get; set; }
        public string ListTitle { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventory
    {
        public Nullable<int> ProductID { get; set; }
        public string Row { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public int LocationID { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int ID { get; set; }
        public Nullable<int> Cat_ID { get; set; }
        public string Item_name { get; set; }
        public string Item_img { get; set; }
        public Nullable<decimal> Item_price { get; set; }
        public Nullable<decimal> Item_quantity { get; set; }
        public string Item_details { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
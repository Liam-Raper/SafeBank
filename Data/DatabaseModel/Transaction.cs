//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Deposeted { get; set; }
        public decimal Withdrawn { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
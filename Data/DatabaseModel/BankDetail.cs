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
    
    public partial class BankDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankDetail()
        {
            this.AccountDetails = new HashSet<AccountDetail>();
            this.EmployeeLocations = new HashSet<EmployeeLocation>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int BrancheDetailsId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountDetail> AccountDetails { get; set; }
        public virtual BrancheDetail BrancheDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLocation> EmployeeLocations { get; set; }
    }
}

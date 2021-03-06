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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.UserAccountAccesses = new HashSet<UserAccountAccess>();
            this.Employees = new HashSet<Employee>();
            this.Customers = new HashSet<Customer>();
        }
    
        public int Id { get; set; }
        public int SecurityQuestionAnswerId { get; set; }
        public int UserAndPasswordId { get; set; }
        public int UserActivityId { get; set; }
        public int UserDetailsId { get; set; }
        public int RoleId { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
        public virtual UserActivity UserActivity { get; set; }
        public virtual UserAndPassword UserAndPassword { get; set; }
        public virtual UserSecurityQuestionAndAnswer UserSecurityQuestionAndAnswer { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAccountAccess> UserAccountAccesses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}

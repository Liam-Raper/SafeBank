using System.Collections.Generic;

namespace SafeBank.Models.Employee
{
    public class EmployeesDetails
    {
        public int BankId { get; set; }
        public int BranchId { get; set; }
        public int LoggedInEmployeeId { get; set; }
        public IEnumerable<EmployeeDetails> EmployeeDetailses { get; set; }
    }
}
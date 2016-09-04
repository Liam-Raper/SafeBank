using System.Collections.Generic;
using System.ComponentModel;
using SafeBank.Models.User;

namespace SafeBank.Models.Employee
{
    public class EditEmployeeRole
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Username { get; set; }
        public IEnumerable<RolesDetails> Roles { get; set; }
        [DisplayName("Select a roles")]
        public string Role { get; set; }
    }
}
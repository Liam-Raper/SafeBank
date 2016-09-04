namespace SafeBank.Models.Employee
{
    public class EmployeeDetails
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string GivenName { private get; set; }
        public string FamilyName { private get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public string FullName => GivenName + ", " + FamilyName;
    }
}
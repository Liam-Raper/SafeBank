﻿namespace SafeBank.Models
{
    public class EmployeeDetails
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string FullName => GivenName + ", " + FamilyName;
    }
}
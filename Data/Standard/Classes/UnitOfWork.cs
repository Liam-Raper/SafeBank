using Data.Bank.Classes;
using Data.Bank.Interfaces;
using Data.DatabaseModel;
using Data.Employees.Classes;
using Data.Employees.Interfaces;
using Data.Security.Membership.Classes;
using Data.Security.Membership.Interfaces;
using Data.Standard.Interfaces;

namespace Data.Standard.Classes
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Entities _database;

        public UnitOfWork()
        {
            _database = new Entities();
            User = new UserTables(_database.Users);
            SecurityQuestion = new SecurityQuestionTables(_database.SecurityQuestions);
            Role = new RoleTable(_database.Roles);
            OrganisationTable = new OrganisationTable(_database.OrganisationDetails);
            BranchTable = new BranchTable(_database.BrancheDetails);
            BankTable = new BankTable(_database.BankDetails);
            EmployeeTable = new EmployeeTable(_database.Employees);
            EmployeeLocationTable = new EmployeeLocationTable(_database.EmployeeLocations);
        }

        public IUserTables<int, User> User { get; }
        public ISecurityQuestionTable<int, SecurityQuestion> SecurityQuestion { get; }
        public IRoleTable<int, Role> Role { get; }
        public IOrganisationTable<int, OrganisationDetail> OrganisationTable { get; }
        public IBranchTable<int, BrancheDetail> BranchTable { get; }
        public IBankTable<int, BankDetail> BankTable { get; }
        public IEmployeeTable<int, Employee> EmployeeTable { get; }
        public IEmployeeLocationTable<int, EmployeeLocation> EmployeeLocationTable { get; }

        public void Commit()
        {
            _database.SaveChanges();
        }
    }
}
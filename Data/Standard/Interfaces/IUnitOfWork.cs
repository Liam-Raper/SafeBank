using Data.Account.Interfaces;
using Data.Bank.Interfaces;
using Data.Customer.Interfaces;
using Data.DatabaseModel;
using Data.Employees.Interfaces;
using Data.Security.Membership.Interfaces;

namespace Data.Standard.Interfaces
{
    public interface IUnitOfWork
    {
        IUserTables<int,User> User { get; }
        ISecurityQuestionTable<int,SecurityQuestion> SecurityQuestion { get; }
        IRoleTable<int, Role> Role { get; }
        IOrganisationTable<int, OrganisationDetail> OrganisationTable { get; }
        IBranchTable<int, BrancheDetail> BranchTable { get; }
        IBankTable<int, BankDetail> BankTable { get; }
        IEmployeeTable<int, Employee> EmployeeTable { get; }
        IEmployeeLocationTable<int, EmployeeLocation> EmployeeLocationTable { get; }
        ICustomerTable<int,DatabaseModel.Customer> CustomerTable { get; }
        IAccountTable<int,DatabaseModel.Account> AccountTable { get; }
        IAccountTypeTable<int,DatabaseModel.AccountType> AccountTypeTable { get; }
        IAccountAccessLevelTable<int,DatabaseModel.AccessLevel> AccessLevelTable { get; }
        void Commit();
    }
}
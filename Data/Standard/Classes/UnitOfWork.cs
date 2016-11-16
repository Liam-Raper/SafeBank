using System;
using Data.Account.Classes;
using Data.Account.Interfaces;
using Data.Bank.Classes;
using Data.Bank.Interfaces;
using Data.Customer.Classes;
using Data.Customer.Interfaces;
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
            CustomerTable = new CustomerTable(_database.Customers);
            AccountTable = new AccountTable(_database.Accounts);
            AccountTypeTable = new AccountTypeTable(_database.AccountTypes);
            AccessLevelTable = new AccountAccessLevelTable(_database.AccessLevels);
            UserAccountAccessTable = new UserAccountAccessTable(_database.UserAccountAccesses);
            AccoutTransactionTable = new AccoutTransactionTable(_database.Transactions);
        }

        public IUserTables<int, User> User { get; }
        public ISecurityQuestionTable<int, SecurityQuestion> SecurityQuestion { get; }
        public IRoleTable<int, Role> Role { get; }
        public IOrganisationTable<int, OrganisationDetail> OrganisationTable { get; }
        public IBranchTable<int, BrancheDetail> BranchTable { get; }
        public IBankTable<int, BankDetail> BankTable { get; }
        public IEmployeeTable<int, Employee> EmployeeTable { get; }
        public IEmployeeLocationTable<int, EmployeeLocation> EmployeeLocationTable { get; }
        public ICustomerTable<int, DatabaseModel.Customer> CustomerTable { get; }
        public IAccountTable<int, DatabaseModel.Account> AccountTable { get; }
        public IAccountTypeTable<int, AccountType> AccountTypeTable { get; }
        public IAccountAccessLevelTable<int, AccessLevel> AccessLevelTable { get; }
        public IUserAccountAccessTable<int, UserAccountAccess> UserAccountAccessTable { get; }
        public IAccoutTransactionTable<int, Transaction> AccoutTransactionTable { get; }

        public void Commit()
        {
            _database.SaveChanges();
        }
    }
}
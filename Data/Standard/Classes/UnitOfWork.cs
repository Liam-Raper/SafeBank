using System;
using Data.Accounts.Bank.Classes;
using Data.Accounts.Bank.Interfaces;
using Data.DatabaseModel;
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
        }

        public IUserTables<int, User> User { get; }
        public ISecurityQuestionTable<int, SecurityQuestion> SecurityQuestion { get; }
        public IRoleTable<int, Role> Role { get; }
        public IOrganisationTable<int, OrganisationDetail> OrganisationTable { get; }
        public IBranchTable<int, BrancheDetail> BranchTable { get; }
        public IBankTable<int, BankDetail> BankTable { get; }

        public void Commit()
        {
            _database.SaveChanges();
        }
    }
}
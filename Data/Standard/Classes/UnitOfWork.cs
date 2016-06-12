using Data.DatabaseModel;
using Data.Security.Account.Classes;
using Data.Security.Account.Interfaces;
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
            Account = new AccountTable(_database.Accounts);
            UserAccountAccess = new UserAccountAccessTable(_database.UserAccountAccesses);
        }

        public IUserTables<int, User> User { get; }
        public ISecurityQuestionTable<int, SecurityQuestion> SecurityQuestion { get; }
        public IRoleTable<int, Role> Role { get; }
        public IAccountTable<int, Account> Account { get; }
        public IUserAccountAccessTable<int, UserAccountAccess> UserAccountAccess { get; }

        public void Commit()
        {
            _database.SaveChanges();
        }
    }
}
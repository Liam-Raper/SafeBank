using Data.DatabaseModel;
using Data.Security.Account.Classes;
using Data.Security.Account.Interfaces;
using Data.Security.Membership.Interfaces;

namespace Data.Standard.Interfaces
{
    public interface IUnitOfWork
    {
        IUserTables<int,User> User { get; }
        ISecurityQuestionTable<int,SecurityQuestion> SecurityQuestion { get; }
        IRoleTable<int, Role> Role { get; }
        IAccountTable<int,Account> Account { get; }
        IUserAccountAccessTable<int, UserAccountAccess> UserAccountAccess { get; }
        void Commit();
    }
}
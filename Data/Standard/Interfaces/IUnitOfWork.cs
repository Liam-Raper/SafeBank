using Data.Accounts.Bank.Interfaces;
using Data.DatabaseModel;
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
        void Commit();
    }
}
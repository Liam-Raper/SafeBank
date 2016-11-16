using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<AccountType> GetAllAccountTypes();
        void CreateAccountForCustomer(string customerUsername, int bankId, AccountBO account);
        void GiveUserAccessToAccount(string username, string accessLevel, int accountNumber);
        void RemoveUserAccessToAccount(string username, int accountNumber);
        bool AccountExist(int accountNumber, int bankId);
        IEnumerable<AccountBO> GetAccountsForACustomer(int customerId);
        void DeleteAccount(int id);
    }
}
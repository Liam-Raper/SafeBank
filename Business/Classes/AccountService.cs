using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using Data.Standard.Interfaces;
using Data.DatabaseModel;

namespace Business.Classes
{
    public class AccountService : IAccountService
    {

        private IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AccountExist(int accountNumber, int bankId)
        {
            return _unitOfWork.AccountTable.GetAll().Any(x => x.AccountDetail.AccountNumber == accountNumber && x.AccountDetail.BankId == bankId);
        }

        public void CreateAccountForCustomer(string customerUsername, int bankId, AccountBO account)
        {
            var accountData = new Account()
            {
                AccountDetail = new AccountDetail
                {
                    AccountName = account.Name,
                    AccountNumber = account.Number,
                    Balance = 0,
                    Overdraft = 0,
                    BankId = bankId
                },
                AccountType = _unitOfWork.AccountTypeTable.GetAll().Single(x => x.Name == account.Type.Name)
            };
            _unitOfWork.AccountTable.AddSingle(accountData);
            _unitOfWork.Commit();
        }

        public void DeleteAccount(int id)
        {
            _unitOfWork.AccountTable.DeleteSingle(id);
            _unitOfWork.Commit();
        }

        public IEnumerable<AccountBO> GetAccountsForACustomer(int customerId)
        {
            var customer = _unitOfWork.CustomerTable.GetAll().Single(x => x.Id == customerId);
            var accountAccessList = customer.User.UserAccountAccesses;
            var resultList = new List<AccountBO>();
            foreach (var accountAccess in accountAccessList)
            {
                var account = accountAccess.Account;
                resultList.Add(new AccountBO
                {
                    Id = account.Id,
                    Name = account.AccountDetail.AccountName,
                    Number = account.AccountDetail.AccountNumber,
                    Type = new Models.AccountType
                    {
                        Name = account.AccountType.Name
                    }
                });
            }
            return resultList;
        }

        public IEnumerable<Models.AccountType> GetAllAccountTypes()
        {
            return _unitOfWork.AccountTypeTable.GetAll().Select(x => new Models.AccountType
            {
                Name = x.Name
            });
        }

        public IEnumerable<TransactionBO> GetTransactionsForAccount(int id)
        {
            return _unitOfWork.AccoutTransactionTable.GetAll().Where(x => x.AccountId == id).Select(x => new TransactionBO
            {
                Deposeted = x.Deposeted,
                Withdrawn = x.Withdrawn
            });
        }

        public void GiveUserAccessToAccount(string username, string accessLevel, int accountNumber)
        {
            var account = _unitOfWork.AccountTable.GetAll().Single(x => x.AccountDetail.AccountNumber == accountNumber);
            var access = _unitOfWork.AccessLevelTable.GetAll().Single(x => x.Name == accessLevel);
            var user = _unitOfWork.User.GetAll().Single(x => x.UserDetail.Username == username);
            if(account.UserAccountAccesses.Any(x => x.UserId == user.Id))
            {
                account.UserAccountAccesses.Single(x => x.UserId == user.Id).AccessLevelId = access.Id;
            }
            else
            {
                access.UserAccountAccesses.Add(new UserAccountAccess
                {
                    UserId = user.Id,
                    AccountId = account.Id,
                    AccessLevelId = access.Id
                });
            }
            _unitOfWork.Commit();
        }

        public void RemoveUserAccessToAccount(string username, int accountNumber)
        {
            var account = _unitOfWork.AccountTable.GetAll().Single(x => x.AccountDetail.AccountNumber == accountNumber);
            var access = account.UserAccountAccesses.Single(x => x.User.UserDetail.Username == username);
            _unitOfWork.UserAccountAccessTable.DeleteSingle(access.Id);
            _unitOfWork.Commit();
        }

        public void UpdateAccount(AccountBO accountBo)
        {
            var account = _unitOfWork.AccountTable.GetSingle(accountBo.Id);
            account.AccountDetail.AccountName = accountBo.Name;
            _unitOfWork.Commit();
        }
    }
}
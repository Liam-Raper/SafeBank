using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using Data.Standard.Interfaces;

namespace Business.Classes
{
    public class AccountService : IAccountService
    {

        private IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AccountType> GetAllAccountTypes()
        {
            return _unitOfWork.AccountTypeTable.GetAll().Select(x => new AccountType
            {
                Name = x.Name
            });
        }
    }
}
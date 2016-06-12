using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Models;
using Data.Standard.Interfaces;

namespace Business.Classes
{
    public class UserService : IUserService
    {

        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserAccountsBo GetUserAccounts(string username)
        {
            return new UserAccountsBo
            {
                Accounts = _unitOfWork.UserAccountAccess.GetAll()
                    .Where(x => x.User.UserDetail.Username == username)
                    .ToArray()
                    .Select(
                        x =>
                            new UserAccountBO(x.Account.AccountDetail.AccountName,
                                x.Account.AccountDetail.AccountName,
                                x.Account.AccountDetail.Balance,
                                x.Account.AccountDetail.Overdraft,
                                x.Account.AccountDetail.BankDetail.Code,
                                x.Account.AccountDetail.BankDetail.BrancheDetail.Code,
                                x.Account.AccountDetail.BankDetail.BrancheDetail.OrganisationDetail.Code,
                                x.AccessLevel.Name))
                    .ToArray()
            };

        }
    }
}

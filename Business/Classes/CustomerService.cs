using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using Data.Standard.Interfaces;

namespace Business.Classes
{
    public class CustomerService : ICustomerService
    {

        private IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CustomerBO> GetCustomers(int bankId)
        {
            return _unitOfWork.CustomerTable.GetAll()
                    .Where(x => x.User.UserAccountAccesses.Any(y => y.Account.AccountDetail.BankId == bankId))
                    .Select(x => new CustomerBO
                    {
                        id = x.Id,
                        FamilyName = x.CustomerDetail.Family_name,
                        GivenName = x.CustomerDetail.Given_name,
                        Phone = x.CustomerDetail.Phone,
                        Email = x.CustomerDetail.Email
                    }).ToList();
        }
    }
}
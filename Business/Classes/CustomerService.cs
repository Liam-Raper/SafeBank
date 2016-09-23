using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using Data.Standard.Interfaces;
using Data.DatabaseModel;

namespace Business.Classes
{
    public class CustomerService : ICustomerService
    {

        private IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCustomer(CustomerBO customer)
        {
            var customerData = new Customer()
            {
                CustomerDetail = new CustomerDetail
                {
                    Given_name = customer.GivenName,
                    Family_name = customer.FamilyName,
                    Phone = customer.Phone,
                    Email = customer.Email
                },
                User = _unitOfWork.User.GetAll().Single(x => x.UserDetail.Username == customer.Username)
            };
            _unitOfWork.CustomerTable.AddSingle(customerData);
            _unitOfWork.Commit();
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
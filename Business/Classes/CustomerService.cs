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

        public bool CustomerExist(int customerId)
        {
            return _unitOfWork.CustomerTable.GetAll().Any(x => x.Id == customerId);
        }

        public void DeleteCustomer(int id)
        {
            var customer = _unitOfWork.CustomerTable.GetSingle(id);
            _unitOfWork.CustomerTable.DeleteSingle(customer.Id);
            _unitOfWork.Commit();
        }

        public int GetCustomerId(string userName)
        {
            return _unitOfWork.CustomerTable.GetAll().Single(x => x.User.UserDetail.Username == userName).Id;
        }

        public CustomerBO GetCustomer(int customerId)
        {
            var customer = _unitOfWork.CustomerTable.GetSingle(customerId);
            return new CustomerBO
            {
                id = customer.Id,
                Username = customer.User.UserDetail.Username,
                Email = customer.CustomerDetail.Email,
                FamilyName = customer.CustomerDetail.Family_name,
                GivenName = customer.CustomerDetail.Given_name,
                Phone = customer.CustomerDetail.Phone
            };
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

        public void UpdateCustomer(CustomerBO customerBo)
        {
            var customer = _unitOfWork.CustomerTable.GetSingle(customerBo.id);
            customer.CustomerDetail.Family_name = customerBo.FamilyName;
            customer.CustomerDetail.Given_name = customerBo.GivenName;
            _unitOfWork.Commit();
        }
    }
}
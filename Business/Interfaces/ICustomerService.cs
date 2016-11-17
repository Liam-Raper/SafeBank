using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerBO> GetCustomers(int bankId);
        void AddCustomer(CustomerBO customer);
        bool CustomerExist(int customerId);
        CustomerBO GetCustomer(int customerId);
        int GetCustomerId(string userName);
        void UpdateCustomer(CustomerBO employee);
        void DeleteCustomer(int id);
    }
}
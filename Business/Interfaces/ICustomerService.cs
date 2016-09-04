using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerBO> GetCustomers(int bankId);
    }
}
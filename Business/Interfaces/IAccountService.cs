using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<AccountType> GetAllAccountTypes();
    }
}
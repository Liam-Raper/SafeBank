using Business.Models;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IBankService
    {
        IEnumerable<BankBO> GetAllBanksUnderABranch(int BranchId);
        bool BankExist(int BranchId, string name);
        bool BankCodeExist(int BranchId, int code);
        void AddBank(int BranchId, BankBO bank);
    }
}

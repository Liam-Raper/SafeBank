using Business.Models;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IBankService
    {
        IEnumerable<BankBO> GetAllBanksUnderABranch(int branchId);
        bool BankExist(int branchId, string name);
        bool BankCodeExist(int branchId, int code);
        bool BankIdExists(int id);
        void AddBank(int branchId, BankBO bank);
        BankBO GetBank(int id);
        void UpdateBank(BankBO bank);
        int GetBranchId(int bankId);
        void DeleteBank(int id);
    }
}

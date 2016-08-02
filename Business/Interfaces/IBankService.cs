using Business.Models;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IBankService
    {
        IEnumerable<BankBO> GetAllBanksUnderABranch(int BranchId);
        bool BankExist(int BranchId, string name);
        bool BankCodeExist(int BranchId, int code);
        bool BankIdExists(int id);
        void AddBank(int BranchId, BankBO bank);
        BankBO GetBank(int id);
        void UpdateBranch(BankBO bank);
        int GetBranchId(int bankId);
        void DeleteBank(int id);
    }
}

using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Data.DatabaseModel;
using Data.Standard.Interfaces;

namespace Business.Classes
{
    public class BankService : IBankService
    {

        private IUnitOfWork _unitOfWork;

        public BankService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddBank(int BranchId, BankBO bank)
        {
            _unitOfWork.BankTable.AddSingle(new BankDetail
            {
                Code = bank.Code,
                Name = bank.Name,
                BrancheDetailsId = BranchId
            });
            _unitOfWork.Commit();
        }
        
        public bool BankCodeExist(int BranchId, int code)
        {
            return _unitOfWork.BankTable.GetAll().Any(x => x.Code == code && x.BrancheDetailsId == BranchId);
        }

        public bool BankExist(int BranchId, string name)
        {
            return _unitOfWork.BankTable.GetAll().Any(x => x.Name == name && x.BrancheDetailsId == BranchId);
        }

        public IEnumerable<BankBO> GetAllBanksUnderABranch(int BranchId)
        {
            return
                _unitOfWork.BranchTable.GetAll()
                    .Single(x => x.Id == BranchId)
                    .BankDetails.Select(bankDetail => new BankBO
                    {
                        Id = bankDetail.Id,
                        Name = bankDetail.Name,
                        Code = bankDetail.Code
                    }).ToArray();
        }
    }
}

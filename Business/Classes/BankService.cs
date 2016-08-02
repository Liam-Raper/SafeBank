using Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Business.Models;
using Data.DatabaseModel;
using Data.Standard.Interfaces;
using System;

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

        public bool BankIdExists(int id)
        {
            return _unitOfWork.BranchTable.GetAll().Any(x => x.Id == id);
        }

        public void DeleteBank(int id)
        {
            _unitOfWork.BankTable.DeleteSingle(id);
            _unitOfWork.Commit();
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
                        Code = bankDetail.Code,
                        EmployeeCount = bankDetail.EmployeeLocations.Count
                    }).ToArray();
        }

        public BankBO GetBank(int id)
        {
            var bank = _unitOfWork.BankTable.GetSingle(id);
            return new BankBO
            {
                Id = bank.Id,
                Code = bank.Code,
                Name = bank.Name
            };
        }

        public int GetBranchId(int bankId)
        {
            return _unitOfWork.BankTable.GetSingle(bankId).BrancheDetailsId;
        }

        public void UpdateBranch(BankBO bank)
        {
            var bankDetails = new BankDetail
            {
                Name = bank.Name,
                Code = bank.Code
            };
            _unitOfWork.BankTable.UpdateSingle(bank.Id, bankDetails);
            _unitOfWork.Commit();
        }
    }
}

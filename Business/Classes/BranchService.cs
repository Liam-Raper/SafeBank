using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using Data.DatabaseModel;
using Data.Standard.Interfaces;

namespace Business.Classes
{
    public class BranchService : IBranchService
    {

        private IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BranchBO> GetAllBranchesAtAnOrganisation(int organisationId)
        {
            return
                _unitOfWork.OrganisationTable.GetAll()
                    .Single(x => x.Id == organisationId)
                    .BrancheDetails.Select(brancheDetail => new BranchBO
                    {
                        Id = brancheDetail.Id,
                        Name = brancheDetail.Name,
                        Code = brancheDetail.Code
                    }).ToArray();
        }

        public int GetNumberOfBranches(int organisationId)
        {
            return _unitOfWork.OrganisationTable.GetAll()
                .Single(x => x.Id == organisationId)
                .BrancheDetails.Count;
        }

        public bool BranchExist(int organisationId, string name)
        {
            return _unitOfWork.BranchTable.GetAll().Any(x => x.Name == name && x.OrganisationDetailsId == organisationId);
        }

        public bool BranchCodeExist(int organisationId, int code)
        {
            return _unitOfWork.BranchTable.GetAll().Any(x => x.Code == code && x.OrganisationDetailsId == organisationId);
        }

        public bool BranchIdExists(int id)
        {
            return _unitOfWork.BranchTable.GetAll().Any(x => x.Id == id);
        }

        public void AddBranch(int organisationId, BranchBO branch)
        {
            _unitOfWork.BranchTable.AddSingle(new BrancheDetail
            {
                Code = branch.Code,
                Name = branch.Name,
                OrganisationDetailsId = organisationId
            });
            _unitOfWork.Commit();
        }

        public void UpdateBranch(BranchBO branch)
        {
            var branchDetails = new BrancheDetail
            {
                Name = branch.Name,
                Code = branch.Code
            };
            _unitOfWork.BranchTable.UpdateSingle(branch.Id, branchDetails);
            _unitOfWork.Commit();
        }

        public BranchBO GetBranch(int id)
        {
            var branch = _unitOfWork.BranchTable.GetSingle(id);
            return new BranchBO
            {
                Id = branch.Id,
                Code = branch.Code,
                Name = branch.Name
            };
        }

        public int GetOrganisationId(int branchId)
        {
            return _unitOfWork.BranchTable.GetSingle(branchId).OrganisationDetailsId;
        }

        public void DeleteBranch(int id)
        {
            _unitOfWork.BranchTable.DeleteSingle(id);
            _unitOfWork.Commit();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Business.Models;
using Data.DatabaseModel;
using Data.Standard.Interfaces;

namespace Business.Classes
{
    public class OrganisationService : IOrganisationService
    {

        private IUnitOfWork _unitOfWork;

        public OrganisationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<OrganisationBO> GetOrganisations()
        {
            return _unitOfWork.OrganisationTable.GetAll()
                .Select(x => new OrganisationBO {Id = x.Id, Code = x.Code, Name = x.Name, BranchCount = x.BrancheDetails.Count})
                .ToArray();
        }

        public bool OrganisationExist(string name)
        {
            return _unitOfWork.OrganisationTable.GetAll().Any(x => x.Name == name);
        }

        public bool OrganisationCodeExist(int code)
        {
            return _unitOfWork.OrganisationTable.GetAll().Any(x => x.Code == code);
        }

        public bool OrganisationIdExists(int id)
        {
            return _unitOfWork.OrganisationTable.GetAll().Any(x => x.Id == id);
        }

        public void AddOrganisation(OrganisationBO organisation)
        {
            _unitOfWork.OrganisationTable.AddSingle(new OrganisationDetail
            {
                Code = organisation.Code,
                Name = organisation.Name
            });
            _unitOfWork.Commit();
        }

        public void UpdateOrganisation(OrganisationBO organisation)
        {
            var org = new OrganisationDetail
            {
                Name = organisation.Name,
                Code = organisation.Code
            };
            _unitOfWork.OrganisationTable.UpdateSingle(organisation.Id, org);
            _unitOfWork.Commit();
        }

        public OrganisationBO GetOrganisation(int id)
        {
            var org = _unitOfWork.OrganisationTable.GetSingle(id);
            return new OrganisationBO
            {
                Id = org.Id,
                Code = org.Code,
                Name = org.Name
            };
        }

        public void DeleteOrganisation(int id)
        {
            _unitOfWork.OrganisationTable.DeleteSingle(id);
            _unitOfWork.Commit();
        }
    }
}
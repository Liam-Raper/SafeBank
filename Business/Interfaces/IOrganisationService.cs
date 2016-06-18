using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface IOrganisationService
    {
        IEnumerable<OrganisationBO> GetOrganisations();
        bool OrganisationExist(string name);
        bool OrganisationCodeExist(int code);
        bool OrganisationIdExists(int id);
        void AddOrganisation(OrganisationBO organisation);
        void UpdateOrganisation(OrganisationBO organisation);
        OrganisationBO GetOrganisation(int id);
        void DeleteOrganisation(int id);
    }
}
using System.Collections.Generic;
using Business.Models;

namespace Business.Interfaces
{
    public interface IBranchService
    {
        IEnumerable<BranchBO> GetAllBranchesAtAnOrganisation(int organisationId);
        bool BranchExist(int organisationId, string name);
        bool BranchCodeExist(int organisationId, int code);
        bool BranchIdExists(int id);
        void AddBranch(int organisationId, BranchBO branch);
    }
}
using System.Collections.Generic;

namespace SafeBank.Models.Branch
{
    public class BranchesDetails
    {
        public int OrganisationId { get; set; }
        public IEnumerable<BranchDetails> BranchDetailses { get; set; }
    }
}
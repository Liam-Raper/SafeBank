using System.Collections.Generic;

namespace SafeBank.Models
{
    public class BranchesDetails
    {
        public int OrganisationId { get; set; }
        public IEnumerable<BranchDetails> BranchDetailses { get; set; }
    }
}
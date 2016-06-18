using System.Collections.Generic;

namespace SafeBank.Models
{
    public class BankesDetails
    {
        public int BranchId { get; set; }
        public IEnumerable<BankDetails> BankDetailses { get; set; }
    }
}
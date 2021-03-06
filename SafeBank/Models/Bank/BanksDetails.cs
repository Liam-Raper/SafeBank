﻿using System.Collections.Generic;

namespace SafeBank.Models.Bank
{
    public class BanksDetails
    {
        public int BranchId { get; set; }
        public int OrganisationId { get; set; }
        public IEnumerable<BankDetails> BankDetailses { get; set; }
    }
}
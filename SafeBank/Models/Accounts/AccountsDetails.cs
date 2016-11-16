using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeBank.Models.Accounts
{
    public class AccountsDetails
    {
        public int CustomerId { get; set; }
        public IEnumerable<AccountDetails> AccountDetailses { get; set; }
    }
}
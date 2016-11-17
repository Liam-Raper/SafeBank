using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeBank.Models.Accounts
{
    public class AccountsTransactions
    {

        public int accountId { get; set; }
        public int customerId { get; set; }
        public string AccountName { get; set; }
        public IEnumerable<AccountsTransaction> Transactions { get; set; }

    }
}
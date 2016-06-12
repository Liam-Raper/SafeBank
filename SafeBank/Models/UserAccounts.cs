using System.Collections.Generic;
using System.Linq;
using Business.Models;

namespace SafeBank.Models
{
    public class UserAccounts
    {
        public IEnumerable<UserAccount> Accounts;

        public UserAccounts(UserAccountsBo userAccountsBo)
        {
            Accounts = userAccountsBo.Accounts.Select(x => new UserAccount(x)).ToArray();
        }

    }
}
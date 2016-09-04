using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Business.Interfaces;

namespace SafeBank.Models.Accounts
{
    public class AddAccountDetails
    {
        public IEnumerable<string> AccountTypesList { get; private set; }

        public AddAccountDetails()
        {
            var accountService = DependencyResolver.Current.GetService<IAccountService>();
            AccountTypesList = accountService.GetAllAccountTypes().Select(x => x.Name);
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an account type")]
        [DisplayName("Account type")]
        public string AccountType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an account number")]
        [DisplayName("Account number")]
        public int? AccountNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an account name")]
        [DisplayName("Account name")]
        public string AccountName { get; set; }

    }
}
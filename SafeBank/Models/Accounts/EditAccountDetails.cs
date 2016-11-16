using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafeBank.Models.Accounts
{
    public class EditAccountDetails
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an account name")]
        [DisplayName("Account name")]
        public string AccountName { get; set; }

        public int CustomerId { get; set; }
        public int AccountId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafeBank.Models.Accounts
{
    public class AccountDeposeted
    {
        public int accountId { get; set; }
        public int customerId { get; set; }

        [DisplayName("Account")]
        public string accountName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an account type")]
        [Range(0.01,1000000,ErrorMessage ="You tryed to deposet a value that is not allowed")]
        [DisplayName("Deposet ammount")]
        public decimal ammount { get; set; }

    }
}
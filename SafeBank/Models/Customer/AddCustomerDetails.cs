using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SafeBank.Models.Accounts;
using SafeBank.Models.User;

namespace SafeBank.Models.Customer
{
    public class AddCustomerDetails
    {
        private UserJoinDetails _userDetails;
        public UserJoinDetails UserDetails
        {
            get
            {
                if (_userDetails == null) _userDetails = new UserJoinDetails();
                return _userDetails;
            }
        }

        private AddAccountDetails _accountDetails;
        public AddAccountDetails AccountDetails
        {
            get
            {
                if (_accountDetails == null) _accountDetails = new AddAccountDetails();
                return _accountDetails;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a given name for the new customer")]
        [DisplayName("Given name")]
        public string GivenName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a family name for the new customer")]
        [DisplayName("Family name")]
        public string FamilyName { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a phone number for the new customer")]
        [DisplayName("Phone number")]
        public string Phone { get; set; }
    }
}

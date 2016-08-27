using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SafeBank.Models
{
    public class AddEmployeeDetails : UserJoinDetails
    {
        public int BankId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a employee code for the new employee")]
        [DisplayName("Employee Code")]
        public int? EmployeeCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a given name for the new employee")]
        [DisplayName("Given name")]
        public string GivenName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a family name for the new employee")]
        [DisplayName("Family name")]
        public string FamilyName { get; set; }

        [Phone]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a phone number for the new employee")]
        [DisplayName("Phone number")]
        public string Phone { get; set; }

    }
}
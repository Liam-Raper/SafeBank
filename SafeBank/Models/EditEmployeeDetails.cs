using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SafeBank.Models
{
    public class EditEmployeeDetails
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a given name for the new employee")]
        [DisplayName("Given name")]
        public string GivenName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a family name for the new employee")]
        [DisplayName("Family name")]
        public string FamilyName { get; set; }

    }
}
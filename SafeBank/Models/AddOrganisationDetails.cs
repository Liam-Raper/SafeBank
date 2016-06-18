using System.ComponentModel.DataAnnotations;

namespace SafeBank.Models
{
    public class AddOrganisationDetails
    {

        [Required(AllowEmptyStrings = false,ErrorMessage = "You did not give a name for the new organisation")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "You did not give a code for the organisation")]
        public int? Code { get; set; }

    }
}
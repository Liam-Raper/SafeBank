using System.ComponentModel.DataAnnotations;

namespace SafeBank.Models
{
    public class AddBranchDetails
    {

        public int OrganisationId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a name for the new branch")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a code for the branch")]
        public int? Code { get; set; }
    }
}
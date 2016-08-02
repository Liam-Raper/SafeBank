using System.ComponentModel.DataAnnotations;

namespace SafeBank.Models
{
    public class EditBankDetails
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a name for the new bank")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You did not give a code for the bank")]
        public int? Code { get; set; }
    }
}
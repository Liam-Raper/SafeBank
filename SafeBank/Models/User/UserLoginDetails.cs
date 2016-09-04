using System.ComponentModel.DataAnnotations;

namespace SafeBank.Models.User
{
    public class UserLoginDetails
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "You must enter a username")]
        public string Username { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d$@$!%*?&]{8,}",ErrorMessage = "The password you entered dose not have the required characters. You must have atleast 1 uppercase, 1 lowercase and 1 number, optionally a special character of $@$!%*?&")]
        public string Password { get; set; }
    }
}

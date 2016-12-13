using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Mvc;
using Security.Interfaces.SecurityQuestions;

namespace SafeBank.Models.User
{
    public class UserJoinDetails
    {

        public IEnumerable<string> QuestionsList { get; private set; }

        public UserJoinDetails()
        {
            var securityQuestions = DependencyResolver.Current.GetService<ISecurityQuestions>();
            QuestionsList = securityQuestions.GetAllSystemQuestions();
        }
        private string _userName;

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a username")]
        [AllowHtml]
        public string Username
        {
            get { return _userName; }
            set { _userName = WebUtility.HtmlEncode(value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d$@$!%*?&]{8,}", ErrorMessage = "The password you entered dose not have the required characters. You must have atleast 1 uppercase, 1 lowercase and 1 number, optionally a special character of $@$!%*?&")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare(nameof(Password),ErrorMessage = "Conferme password has to match the password you entered")]
        [DisplayName("Conferme Password")]
        public string ConfermePassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an email address")]
        [EmailAddress(ErrorMessage = "You did not enter a valid email address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an security question question")]
        [DisplayName("Security question")]
        public string Question { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an security question answer")]
        [DisplayName("Security answer")]
        public string Answer { get; set; }

    }
}
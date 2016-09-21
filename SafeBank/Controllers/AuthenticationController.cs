using System.Web.Mvc;
using System.Web.Security;
using SafeBank.Models.User;
using Security.Interfaces.User;

namespace SafeBank.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserActivities _userActivities;

        public AuthenticationController(IUserActivities userActivities)
        {
            _userActivities = userActivities;
        }

        public ActionResult LogIn()
        {
            return View("LogIn",new UserLoginDetails());
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginDetails loginDetails)
        {
            if (!ModelState.IsValid)
            {
                return View("LogIn", loginDetails);
            }
            if(!Membership.ValidateUser(loginDetails.Username, loginDetails.Password))
            {
                ModelState.AddModelError("UserOrPasswordNoValid", "The username or password you gave are not valid so we can't log you in.");
                return View("LogIn", loginDetails);
            }
            _userActivities.UpdateLoggedInDateTime(loginDetails.Username);
            _userActivities.UpdateLastActionDateTime(loginDetails.Username);
            FormsAuthentication.SetAuthCookie(loginDetails.Username, true);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Join()
        {
            return View("Join", new UserJoinDetails());
        }

        [HttpPost]
        public ActionResult Join(UserJoinDetails joinDetails)
        {
            if (!ModelState.IsValid)
            {
                return View("Join", joinDetails);
            }
            MembershipCreateStatus status;
            Membership.CreateUser(joinDetails.Username, joinDetails.Password, joinDetails.Email, joinDetails.Question,
                joinDetails.Answer, true, out status);
            if (status != MembershipCreateStatus.Success)
            {
                ModelState.AddModelError("UnableToAddUser", "Unable to create a user with the details you gave are you user your not already in the system?");
                return View("Join", joinDetails);
            }
            return RedirectToAction("LogIn");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
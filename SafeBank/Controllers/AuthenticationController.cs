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
            if (!ModelState.IsValid || !Membership.ValidateUser(loginDetails.Username,loginDetails.Password))
            {
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
            MembershipCreateStatus status;
            Membership.CreateUser(joinDetails.Username, joinDetails.Password, joinDetails.Email, joinDetails.Question,
                joinDetails.Answer, true, out status);
            if (!ModelState.IsValid || status != MembershipCreateStatus.Success)
            {
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
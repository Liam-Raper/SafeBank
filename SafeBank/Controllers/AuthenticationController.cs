using System.Web.Mvc;
using System.Web.Security;
using SafeBank.Models;

namespace SafeBank.Controllers
{
    public class AuthenticationController : Controller
    {

        public ActionResult LogIn()
        {
            return View("LogIn",new UserLoginDetails());
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginDetails loginDetails)
        {
            var status = MembershipCreateStatus.ProviderError;
            Membership.CreateUser("test", "test", "test", "test", "test", true, 0, out status);
            if (!ModelState.IsValid)
            {
                return View("LogIn",loginDetails);
            }
            return RedirectToAction("index", "Home");
        }

    }
}
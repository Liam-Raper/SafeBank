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
            if (!ModelState.IsValid)
            {
                return View("LogIn",loginDetails);
            }
            return RedirectToAction("index", "Home");
        }

    }
}
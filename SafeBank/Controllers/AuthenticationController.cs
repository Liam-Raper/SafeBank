using System.Web.Mvc;
using SafeBank.Models;

namespace SafeBank.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult LogIn()
        {
            return View(new UserLoginDetails());
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginDetails loginDetails)
        {
            return View(loginDetails);
        }

    }
}
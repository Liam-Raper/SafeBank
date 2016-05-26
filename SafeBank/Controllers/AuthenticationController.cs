using System.Web.Mvc;

namespace SafeBank.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult LogIn()
        {
            return View();
        }
    }
}
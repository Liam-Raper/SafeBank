using System.Web.Mvc;

namespace SafeBank.Controllers
{
    public class AccountsController : Controller
    {
        [Authorize]
        public ActionResult Accounts()
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace SafeBank.Controllers
{
    public class AccountsController : Controller
    {
        [Authorize(Roles = "Customer")]
        public ActionResult Accounts()
        {
            return View();
        }
    }
}
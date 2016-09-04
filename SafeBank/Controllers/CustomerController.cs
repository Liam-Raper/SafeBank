using System.Web.Mvc;

namespace SafeBank.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Accounts()
        {
            return View();
        }
    }
}
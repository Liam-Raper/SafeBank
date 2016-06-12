using System.Web.Mvc;
using Business.Interfaces;
using SafeBank.Models;

namespace SafeBank.Controllers
{
    public class AccountsController : Controller
    {
        private IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Accounts()
        {
            return View("Accounts",new UserAccounts(_userService.GetUserAccounts(User.Identity.Name)));
        }
    }
}
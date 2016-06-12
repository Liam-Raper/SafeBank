using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
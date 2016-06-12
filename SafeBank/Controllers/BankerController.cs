using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafeBank.Controllers
{
    public class BankerController : Controller
    {
        public ActionResult CustomerManager()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SafeBank
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home",     "home",                       new { controller = "Home",           action = "Index"});
            routes.MapRoute("About",    "about",                      new { controller = "Home",           action = "About" });
            routes.MapRoute("LogIn",    "login",                      new { controller = "Authentication", action = "LogIn" });
            routes.MapRoute("Join",     "join",                       new { controller = "Authentication", action = "Join" });
            routes.MapRoute("Accounts", "Accounts",                   new { controller = "Accounts",       action = "Accounts" });
            routes.MapRoute("Default",  "{controller}/{action}/{id}", new { controller = "Home",           action = "Index", id = UrlParameter.Optional });
        }
    }
}
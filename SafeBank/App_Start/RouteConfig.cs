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

            routes.MapRoute("Home",                   "home",                         new { controller = "Home",           action = "Index"});
            routes.MapRoute("About",                  "about",                        new { controller = "Home",           action = "About" });
            routes.MapRoute("LogIn",                  "login",                        new { controller = "Authentication", action = "LogIn" });
            routes.MapRoute("Join",                   "join",                         new { controller = "Authentication", action = "Join" });
            routes.MapRoute("LogOut",                 "logout",                       new { controller = "Authentication", action = "LogOut" });
            routes.MapRoute("Customer_Accounts",      "accounts",                     new { controller = "Customer",       action = "Accounts" });
            routes.MapRoute("Banker_Accounts",        "customermanager",              new { controller = "Banker",         action = "CustomerManager" });
            routes.MapRoute("Bank_Manager_Accounts",  "employeemanager",              new { controller = "BankManager",    action = "EmployeeManager" });
            routes.MapRoute("Administrator_Accounts", "Dashboard",                    new { controller = "Administrator",  action = "Dashboard" });
            routes.MapRoute("Default",                "{controller}/{action}/{id}",   new { controller = "Home",           action = "Index", id = UrlParameter.Optional });
        }
    }
}
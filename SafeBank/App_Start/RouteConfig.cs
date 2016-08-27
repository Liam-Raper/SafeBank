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

            routes.MapRoute("Home",                                 "home",                         new { controller = "Home",           action = "Index"});
            routes.MapRoute("About",                                "about",                        new { controller = "Home",           action = "About" });
            routes.MapRoute("LogIn",                                "login",                        new { controller = "Authentication", action = "LogIn" });
            routes.MapRoute("Join",                                 "join",                         new { controller = "Authentication", action = "Join" });
            routes.MapRoute("LogOut",                               "logout",                       new { controller = "Authentication", action = "LogOut" });
            routes.MapRoute("Customer_Accounts",                    "accounts",                     new { controller = "Customer",       action = "Accounts" });
            routes.MapRoute("Banker_Accounts",                      "customermanager",              new { controller = "Banker",         action = "CustomerManager" });
            routes.MapRoute("Bank_Manager_Accounts",                "employeemanager",              new { controller = "BankManager",    action = "EmployeeManager" });
            routes.MapRoute("Administrator_Organisations",          "Admin/Organisations",          new { controller = "Administrator",  action = "Dashboard" });
            routes.MapRoute("Administrator_Organisations_Add",      "Admin/Organisations/Add",      new { controller = "Administrator",  action = "AddOrganisation" });
            routes.MapRoute("Administrator_Organisations_Edit",     "Admin/Organisations/Edit",     new { controller = "Administrator",  action = "EditOrganisation" });
            routes.MapRoute("Administrator_Organisations_Delete",   "Admin/Organisations/Delete",   new { controller = "Administrator",  action = "DeleteOrganisation" });
            routes.MapRoute("Administrator_Branches",               "Admin/Branches",               new { controller = "Administrator",  action = "OrganisationBranchesList" });
            routes.MapRoute("Administrator_Branches_Add",           "Admin/Branches/Add",           new { controller = "Administrator",  action = "AddBranch" });
            routes.MapRoute("Administrator_Branches_Edit",          "Admin/Branches/Edit",          new { controller = "Administrator",  action = "EditBranch" });
            routes.MapRoute("Administrator_Branches_Delete",        "Admin/Branches/Delete",        new { controller = "Administrator",  action = "DeleteBranch" });
            routes.MapRoute("Administrator_Banks",                  "Admin/Banks",                  new { controller = "Administrator",  action = "BanksList" });
            routes.MapRoute("Administrator_Banks_Add",              "Admin/Banks/Add",              new { controller = "Administrator",  action = "AddBank" });
            routes.MapRoute("Administrator_Banks_Edit",             "Admin/Banks/Edit",             new { controller = "Administrator",  action = "EditBank" });
            routes.MapRoute("Administrator_Banks_Delete",           "Admin/Banks/Delete",           new { controller = "Administrator",  action = "DeleteBank" });
            routes.MapRoute("Administrator_Employees",              "Admin/Employees",              new { controller = "Administrator",  action = "BankEmployeeList" });
            routes.MapRoute("Administrator_Employees_Add",          "Admin/Employees/Add",          new { controller = "Administrator",  action = "AddEmployee" });
            routes.MapRoute("Administrator_Employees_Edit",         "Admin/Employees/Edit",         new { controller = "Administrator",  action = "EditEmployee" });
            routes.MapRoute("Administrator_Employees_Delete",       "Admin/Employees/Delete",       new { controller = "Administrator",  action = "DeleteEmployee" });
            routes.MapRoute("Default",                              "{controller}/{action}/{id}",   new { controller = "Home",           action = "Index", id = UrlParameter.Optional });
        }
    }
}